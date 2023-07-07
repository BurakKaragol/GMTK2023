using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MrLule.Attributes;

namespace MrLule.Systems.CommandConsole
{
    public class CommandConsole : MonoBehaviour
    {
        [Tooltip("Help and Clear functions are predefined. Function_name - description")]
        [TextArea(10, 10)]
        [SerializeField] private string helpString = "";

        private string inputString = "";
        private string inputHistoryString = "";
        private string suggestionsString = "";
        private string topSuggestion = "";
        private bool isConsoleOpen = false;
        private int inputHistoryIndex = -1;
        private int suggestionCount = 0;
        private List<string> inputHistory = new List<string>();
        private List<string> methodNames = new List<string>();
        private Dictionary<string, MethodInfo> cheatCodes = new Dictionary<string, MethodInfo>();
        private MethodInfo[] methods;
        private string parameters = "";

        public static CommandConsole Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
            }

            methods = Assembly.GetExecutingAssembly().GetTypes().SelectMany(assembly => assembly.GetMethods())
                .Where(method => Attribute.IsDefined(method, typeof(ConsoleCommandAttribute))).ToArray();
            foreach (MethodInfo method in methods)
            {
                ConsoleCommandAttribute[] attributes = method.GetCustomAttributes(typeof(ConsoleCommandAttribute), true) as ConsoleCommandAttribute[];
                if (attributes.Length > 0)
                {
                    parameters = "";
                    cheatCodes.Add(attributes[0].code, method);
                    if (attributes[0].parameters != null)
                    {
                        for (int i = 0; i < attributes[0].parameters.Length; i++)
                        {
                            parameters += " {" + $"{attributes[0].parameters[i]}" + "}";
                        }
                    }
                    methodNames.Add(attributes[0].code + parameters);
                }
            }
        }

        private void Update()
        {
            if (isConsoleOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseConsole();
            }

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                if (isConsoleOpen)
                {
                    CloseConsole();
                }
                else
                {
                    OpenConsole();
                }
            }

            if (isConsoleOpen && Input.GetKeyDown(KeyCode.Return))
            {
                Execute();
            }
        }

        private void Write(string message)
        {
            if (message == "" || message == null)
            {
                return;
            }
            message = "---> " + message;
            inputHistoryString += message + "\n";
        }

        private void OpenConsole()
        {
            isConsoleOpen = true;
            inputString = "";
            inputHistoryIndex = -1;
        }

        private void CloseConsole()
        {
            isConsoleOpen = false;
            inputString = "";
        }

        private void Execute()
        {
            string[] tokens = inputString.Split(' ');
            if (tokens.Length > 0 && tokens[0].StartsWith("/"))
            {
                if (tokens[0].StartsWith("//"))
                {
                    return;
                }

                string command = tokens[0].Substring(1);
                if (cheatCodes.ContainsKey(command))
                {
                    MethodInfo method = cheatCodes[command];
                    object[] args = new object[method.GetParameters().Length];

                    for (int i = 0; i < args.Length; i++)
                    {
                        Type parameterType = method.GetParameters()[i].ParameterType;
                        if (tokens.Length > i + 1)
                        {
                            try
                            {
                                args[i] = Convert.ChangeType(tokens[i + 1], parameterType);
                            }
                            catch (Exception e)
                            {
                                Debug.LogException(e);
                                Debug.LogErrorFormat($"Failed to parse parameter {i + 1} for command {command}");
                                return;
                            }
                        }
                        else
                        {
                            args[i] = parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null;
                        }
                    }

                    inputHistory.Add(inputString);
                    inputString = inputString.Replace("/", ">>> ");
                    inputHistoryString += inputString + "\n";

                    try
                    {
                        string result;
                        result = (string)method.Invoke(this, args);
                        Write(result);
                    }
                    catch (Exception e)
                    {
                        Write($"Failed to execute command {command}");
                        Debug.Log($"Failed: {e}");
                    }
                }
                else
                {
                    inputHistory.Add(inputString);
                    inputString = inputString.Replace("/", ">>> ");
                    inputHistoryString += inputString + "\n";
                    Write($"Unknown command {command}");
                }
            }
            else
            {
                inputHistory.Add(inputString);
                inputString = inputString.Replace("/", ">>> ");
                inputHistoryString += inputString + "\n";
            }
            inputString = "";
        }

        private void OnGUI()
        {
            if (isConsoleOpen)
            {
                if (inputString.StartsWith("/"))
                {
                    UpdateSuggestions();
                }
                else
                {
                    suggestionsString = "";
                    suggestionCount = 0;
                }

                Event e = Event.current;
                if (e.type == EventType.KeyDown)
                {
                    if (e.keyCode == KeyCode.Return)
                    {
                        Execute();
                    }
                    else if (e.keyCode == KeyCode.Escape || e.keyCode == KeyCode.BackQuote)
                    {
                        CloseConsole();
                    }
                    else if (e.keyCode == KeyCode.UpArrow)
                    {
                        if (inputHistoryIndex == -1)
                        {
                            inputHistoryIndex = inputHistory.Count - 1;
                        }
                        else if (inputHistoryIndex > 0)
                        {
                            inputHistoryIndex--;
                        }

                        if (inputHistoryIndex >= 0 && inputHistoryIndex < inputHistory.Count)
                        {
                            inputString = inputHistory[inputHistoryIndex];
                        }
                    }
                    else if (e.keyCode == KeyCode.DownArrow)
                    {
                        if (inputHistoryIndex == -1)
                        {
                            inputString = "";
                        }
                        else if (inputHistoryIndex < inputHistory.Count - 1)
                        {
                            inputHistoryIndex++;
                            inputString = inputHistory[inputHistoryIndex];
                        }
                        else
                        {
                            inputHistoryIndex = -1;
                            inputString = "";
                        }
                    }
                    else if (e.keyCode == KeyCode.Tab)
                    {
                        inputString = topSuggestion;
                    }
                }

                float outputWidth = Screen.width;
                float outputHeight = Screen.height;
                float outputX = (Screen.width - outputWidth) / 2f;
                float outputY = (Screen.height - outputHeight) / 3f;

                float inputWidth = Screen.width;
                float inputHeight = -Screen.height * 0.11f;
                float inputX = (Screen.width - inputWidth) / 2f;
                float inputY = (Screen.height - inputHeight) / 3f;

                float suggestionWidth = Screen.width / 4f;
                float suggestionHeight = -Screen.height * 0.17f;
                float suggestionX = 0;
                float suggestionY = (Screen.height - suggestionHeight) / 3f;

                GUI.SetNextControlName("CheatConsoleOutput");
                string outputStr = GUI.TextArea(new Rect(outputX, outputY, outputWidth, 400f),
                    inputHistoryString.Length != 0 ? inputHistoryString : "Type /help or /h for all commands");

                GUI.SetNextControlName("CheatConsoleInput");
                inputString = GUI.TextField(new Rect(inputX, inputY, inputWidth, 20f), inputString);

                if (suggestionsString.Length != 0)
                {
                    GUI.SetNextControlName("CheatConsoleSuggestion");
                    suggestionsString = GUI.TextArea(new Rect(suggestionX, suggestionY, suggestionWidth, suggestionCount * 20f), suggestionsString);
                }

                GUI.FocusControl("CheatConsoleInput");
            }
        }

        private void UpdateSuggestions()
        {
            suggestionsString = "";
            suggestionCount = 0;
            bool top = true;
            foreach (string method in methodNames)
            {
                string check = inputString.Replace("/", "");
                check = check.Split(" ")[0];
                if (method.StartsWith(check))
                {
                    if (top)
                    {
                        topSuggestion = $"/{method}";
                        top = false;
                    }
                    suggestionsString += $"{method}\n";
                    suggestionCount++;
                }
            }
        }

        [ConsoleCommand("help")]
        public string Help()
        {
            string commands =
                "\nhelp - displays all available codes and their function\n" +
                "clear - clears the console\n" + helpString;
            return commands;
        }

        [ConsoleCommand("clear")]
        public void Clear()
        {
            inputHistoryString = "";
            inputHistoryIndex = -1;
            inputHistory.Clear();
            suggestionCount = 0;
            suggestionsString = "";
        }
    }
}