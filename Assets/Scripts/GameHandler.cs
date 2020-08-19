using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameHandler : MonoBehaviour
{
   public SpriteRenderer[] chatRenderers;
   public SpriteRenderer[] inventoryRenderers;

   private bool IsMouseOverUI() {
       return EventSystem.current.IsPointerOverGameObject();
   }
}
