documentation - https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@6.0/manual/features/session.html

Subsytems and Providers

Unity provides subsystems which are interfaces which contain functions that the providers implement on their end. Providers for android and ios are ARCore and ARKit respectively.


Managers 

Managers help subsystem data be accessible in the unity scene through gameObjects that means every subsystem has an equivalent manager


ARSession

The ARSession component controls the lifecycle of an AR experience by enabling or disabling AR on the target platform.
A session refers to an instance of AR. While other features like plane detection can be independently enabled or disabled, the session controls the lifecycle of all AR features. When you disable the AR Session component, the system no longer tracks features in its environment. Then if you enable it at a later time, the system attempts to recover and maintain previously-detected features.
You can check if a device supports AR or not using the ARSession.


ARInputManager

The ARInputManager component enables world tracking. 
Without the AR Input Manager component, XROrigin can't acquire a world-space pose for the device. The AR Input Manager component is required for AR to function properly.