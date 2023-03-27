using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


namespace Map
{
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }
}

namespace Map
{
    public class MapNode : MonoBehaviour
    {
        public Image sr;
        public Button button;
        public Text buttonText;
        //public SpriteRenderer visitedCircle;
        //public Image visitedCircleImage;

        public Node Node { get; private set; }
        public NodeTemplateSO Template { get; private set; }
        public MapManagerSO mapManager;
        public MapPlayerTrackerSO mapPlayerTracker;
        public GameSceneSO nodeScene;

        public LoadEventChannelSO loadLocation = default;
        

        private float initialScale;
        private const float HoverScaleFactor = 1.2f;
        private float mouseDownTime;

        private const float MaxClickDuration = 0.5f;

        public void SetUp(Node node, NodeTemplateSO template, MapManagerSO mapManager, MapPlayerTrackerSO mapPlayerTracker)
        {
            Node = node;
            Template = template;
            this.mapManager = mapManager;
            this.mapPlayerTracker = mapPlayerTracker;
            nodeScene = mapManager.GetValidScene(template.possibleScenes);
            sr.sprite = template.sprite;
            if (node.nodeType == NodeType.Boss) transform.localScale *= 1.5f;
            initialScale = sr.transform.localScale.x;
            buttonText.text = nodeScene.sceneReference.SubObjectName;
            // visitedCircle.color = MapView.Instance.visitedColor;
            // visitedCircle.gameObject.SetActive(false);
            SetState(NodeStates.Locked);
        }

        public void SetState(NodeStates state)
        {
            //visitedCircle.gameObject.SetActive(false);
            switch (state)
            {
                case NodeStates.Locked:
                    //sr.color = MapView.Instance.lockedColor;
                    button.interactable = false;
                    break;
                case NodeStates.Visited:
                    //sr.color = MapView.Instance.visitedColor;
                    //visitedCircle.gameObject.SetActive(true);
                    break;
                case NodeStates.Attainable:
                    // start pulsating from visited to locked color:
                    //sr.color = MapView.Instance.lockedColor;
                    button.interactable = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public void SelectedNode()
        {
            // user clicked on this node:
            Debug.Log($"{Node.templateName} selected");
            mapPlayerTracker.SelectNode(this);
            loadLocation.RaiseEvent(nodeScene, false, false);
            //StartCoroutine(LoadNextSceneAsync());
        }
/*
        IEnumerator LoadNextSceneAsync()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nodeScene.sceneReference.SubObjectName);
            asyncLoad.completed += hideMapScreen;
            while(!asyncLoad.isDone)
            {
                yield return null;
            }
        }*/

        void hideMapScreen(AsyncOperation obj)
        {
            // mapManager.GetComponentInParent<Canvas>().gameObject.SetActive(false);  
        }

        public void ShowSwirlAnimation()
        {
            //if (visitedCircleImage == null)
            //    return;

            //const float fillDuration = 0.3f;
            //visitedCircleImage.fillAmount = 0;

            //DOTween.To(() => visitedCircleImage.fillAmount, x => visitedCircleImage.fillAmount = x, 1f, fillDuration);
        }
    }
}
