using MrLule.Managers.DialogueMan;
using MrLule.Managers.DialogueMan.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(DialogueNodeGraph))]
public class DialogueGraphEditor : NodeGraphEditor
{
    public override void OnOpen()
    {
        window.titleContent = new GUIContent("Dialogue Graph");

        base.OnOpen();
    }

    public override void AddContextMenuItems(GenericMenu menu, Type compatibleType = null, NodePort.IO direction = NodePort.IO.Input)
    {
        Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);

        Type[] nodeTypes;

        if (compatibleType != null && NodeEditorPreferences.GetSettings().createFilter)
        {
            nodeTypes = NodeEditorUtilities.GetCompatibleNodesTypes(NodeEditorReflection.nodeTypes, compatibleType, direction).OrderBy(GetNodeMenuOrder).ToArray();
        }
        else
        {
            nodeTypes = NodeEditorReflection.nodeTypes.OrderBy(GetNodeMenuOrder).ToArray();
        }

        List<Type> allowedTypes = new List<Type>();
        for (int i = 0; i < nodeTypes.Length; i++)
        {
            if (typeof(IDialogueNode).IsAssignableFrom(nodeTypes[i]))
            {
                allowedTypes.Add(nodeTypes[i]);
            }
        }

        nodeTypes = allowedTypes.ToArray();

        for (int i = 0; i < nodeTypes.Length; i++)
        {
            Type type = nodeTypes[i];

            string path = GetNodeMenuName(type);
            if (string.IsNullOrEmpty(path)) continue;

            XNode.Node.DisallowMultipleNodesAttribute disallowAttrib;
            bool disallowed = false;
            if (NodeEditorUtilities.GetAttrib(type, out disallowAttrib))
            {
                int typeCount = target.nodes.Count(x => x.GetType() == type);
                if (typeCount >= disallowAttrib.max) disallowed = true;
            }

            path = path.Insert(0, "Add ");
            path = path.Replace("Mr Lule/Managers/Dialogue Man/Nodes/", "");
            path = path.Insert(path.Length, " Node");
            if (disallowed) menu.AddItem(new GUIContent(path), false, null);
            else menu.AddItem(new GUIContent(path), false, () => {
                XNode.Node node = CreateNode(type, pos);
                if (node != null) NodeEditorWindow.current.AutoConnect(node);
            });
        }
        menu.AddSeparator("");
        if (NodeEditorWindow.copyBuffer != null && NodeEditorWindow.copyBuffer.Length > 0) menu.AddItem(new GUIContent("Paste"), false, () => NodeEditorWindow.current.PasteNodes(pos));
        else menu.AddDisabledItem(new GUIContent("Paste"));
        menu.AddItem(new GUIContent("Preferences"), false, () => NodeEditorReflection.OpenPreferences());
        menu.AddCustomContextMenuItems(target);
    }
}
