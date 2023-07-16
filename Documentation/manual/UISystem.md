# UI System

## Introduction
Most UI elements during gameplay are spawned in dynamically and are saved as editable prefabs within the _Assets/Prefabs/UI_ path.

To edit a UI element, double click on the prefab to enter an in-context editor.

All _dialogue_ UI elements can be animated, however the default animation triggers are limited to _Show, Hide, Focus, Unfocus_.

Every UI Element in the barebones DialogueUI prefab is referenced in 

### Remember to put resources for Unity UI components here.
https://learn.unity.com/tutorial/ui-components

## DialogueUI overview

### **Alert Panel**
Alert panels are not yet used in the project, but they are editable at this point. They might be used in the future for item pickup or event notifications, maybe room clear notification?

Alert panels consist of the parent panel (contains the background image component) and the child text is assigned as the Alert Message.

It has a UIPanel component connected to it, with _Show, Hide_ animation triggers, alongside _OnOpen(), OnClose(), OnClosed()_ Events.

### **Main Dialogue Panel**

The Main dialogue panel contains all sub dialogue components and also dictates the main background image for dialogue.

It has a UIPanel component connected to it, with _Show, Hide_ animation triggers, alongside _OnOpen(), OnClose(), OnClosed()_ Events.

### **Subtitle Panel**

The Subtitle panel contains all of the conversation (subtitle) text, the continue button and the Portrait Name and Portrait Image. 

It has a StandardUISubtitlePanel component connected to it.
It also holds the references to the Portrait Image, Portrait Name, Continue Button and Subtitle Text
 with _Show, Hide, Focus, Unfocus_ animation triggers, alongside _OnOpen(), OnClose(), OnClosed(), OnFocus(), OnUnfocus_ Events.

### **Menu Panel**

The Menu panel shows up when the player is given a choice, and displays up to four different responses the player can give.

### **Portrait Image & Name**

Recommended that the portrait image is kept square, (1:1 aspect ratio), and that all imported portrait images (and eventually sprite atlasses) are imported square, as a multiple of 4 (i.e. 512, 1024, 2048 ...). 

The portrait name will take its contents from the current _Actor_ in the _Conversation_

### **Subtitle Text**
Subtitle (or conversation) text contains the actual speaking contents of the current node of the dialogue tree. 

It has a TextMeshProTypewriterEffect which can be modified to change the behaviour of how the text shows up, including characters per second (default 50).

### **Continue Button**

The Continue button allows the player to progress the dialogue tree.
If you remove the image and scale the element up you can make something that looks like a clickable zone instead of a physical button (But it might be unclear when the player can progress to the next piece of conversation unless there's some indicator)

## Adding more UI elements
More UI elements can be added to the hierarchy anywhere under one of the main Panels, but the basic skeleton must be intact.

