using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AgePick
{
    public class SpriteText : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var parent = transform.parent;

            var parentRenderer = parent.GetComponent<Renderer>();
            var renderer = GetComponent<Renderer>();
            renderer.sortingLayerID = parentRenderer.sortingLayerID;
            renderer.sortingOrder = parentRenderer.sortingOrder + 1;

            var spriteTransform = parent.transform;
            var text = GetComponent<TextMesh>();

            Text name = this.gameObject.GetComponent<Text>();
            text.text = name.text;
            print("name: "+name.text);
        }

 
    }

}