using System;
using cfg;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using QuantumDialog;
using QuantumDialog.Editor;

namespace NodeCanvas.DialogueTrees
{
    [Name("SayV2")]
    [Description(
        "Make the selected Dialogue Actor talk. You can make the text more dynamic by using variable names and actor names in square brackets\ne.g. [myVarName] or [Global/myVarName]")]
    public class StatementNodeLuban : DTNode
    {
        [SerializeField] public int dialogueID = 0;
        [SerializeField] public Statement statement = new Statement("This is a dialogue text");

        public override bool requireActorSelection
        {
            get { return true; }
        }

        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            if (MainData.Instance != null)
            {
                // 在节点中
                var mainData = MainData.Instance.Database.DialogueContent.GetOrDefault(dialogueID).ZN;
                if (mainData != null)
                {
                    statement.text = mainData;
                }
            }

            var tempStatement = statement.ProcessStatementBrackets(bb, DLGTree);
            DialogueTree.RequestSubtitles(new SubtitlesRequestInfo(finalActor, tempStatement, OnStatementFinish));
            return Status.Running;
        }

        void OnStatementFinish()
        {
            status = Status.Success;
            DLGTree.Continue();
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.BeginVertical(Styles.roundedBox);
            Dialogue showDialogue =
                MainDataEditorLoader.MainDataEditor.Instance.Database.DialogueContent.GetOrDefault(dialogueID);
            if (showDialogue != null)
            {
                statement.text = showDialogue.ZN;
                GUILayout.Label("\"<i> " + statement.text.CapLength(30) + "</i> \"");
            }
            else
            {
                GUILayout.Label("\"<i> " + "Invalid Dialogue ID" + dialogueID + "</i> \"");
            }

            GUILayout.EndVertical();
        }
#endif
    }
}