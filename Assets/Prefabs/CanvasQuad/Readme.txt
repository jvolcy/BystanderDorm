 * CanvasQuad produces a floating canvas in world space that is suitable
 * for use in VR and on a Desktop application.  Use the Inspector
 * PlayMode option to specify the modality.
 * In either case, the floating canvas is parented to the main camera.
 * You may specify the camera in the Inspector or you may let the script
 * find it for you.  The srcipt will first search for active cameras
 * before looking for inactive ones.
 * You specify the distance the canvas should be from the camera and
 * its physical size.  The defaults are 0.5m x 0.5m at a distance of
 * 0.5m from the camera.
 * In XR mode, the script will search for and disable the 
 * "StandaloneInputModule" or "InputSystemUIInputModule" components of
 * the EnventSystem object that is automatically (or manually) created
 * by the UI. See the note here for details:
 * https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/ui-setup.html
 * 
 * NOTE THAT, BY DEFAULT, THE CANVAS IS SCALED AT 0,0,0.  That means
 * that it is not visible for editing.  Change the scale to 1,1,1 while
 * editing the UI.  Reset it to 0,0,0 when done.  That way, the canvas
 * is not visible (in the "hide") state on startup.

HOW TO USE
==========
Simply instantiate the CanvasQuad prefab anywhere in your project.  Access
the CanvasQuad script component to set is public members and use its public
functions.

Public Members
--------------
PlayMode - this enum is set to either XR or DESKTOP.  The way we interact with
buttons and controls on the UI is different in these 2 modes.  Set accordingly.
This should be set in the Inspector, not through script as the configuration
occurs in Start().

Main Camera:
The camera to which this canvas should be parented.  If you don't set this,
the script will try to auto-locate the main camera

Distance From Camera:
The distance from the camera where the floating canvas will appear.  0.5m is
the default.

Canvas Scale:
The size of the canvas.  The canvas is square.  So, a value of 0.5 here means
that the canvas will have a physical size of 0.5 x 0.5 m.


HOW TO MODIFY
=============
The default canvas contains a label and button.  You may modify this as needed
for your application.  Note that by default, the Canvas child object of the
CanvasQuad is scaled to 0,0,0.  That means it is not visible for editing.
Change this to 1,1,1 to edit the canvas.  When done, reset it to 0,0,0 so that
the canvas is not visible on startup.
