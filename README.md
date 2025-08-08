# Objectives
- To build a basic avatar customization system that has changeable apparel parts
- To animate avatar without breaking mentioned parts
- To spawn a number of random avatars on the scene
- To have some form of basic rendering optimization

# System Design
Given an avatar prefab that is barebones and naked, and numerous apparel parts prefab like shirts, pants, shoes etc. My idea is to have a script that will attach these apparel to the parts they belong to, hence the introduction of `PartAttacher`.

## PartAttacher.cs
Component that is placed onto avatar's part renderer, such as torso and hips. Label the component with an enum for easier and quick setup. Get an apparel prefab through a function and used its sharedMaterials into a renderer instantiated by the script, while at the same time attaching the part's bones into the renderer.

## AvatarController.cs
Stores all the `PartAttacher` of different `SlotType`, responsible to pass in apparel part prefabs into `PartAttacher` via UI buttons or randomizer (`ApparelDefault.cs`). Should be the main controller script and can divide tasks into other sub controller scripts, such as `AvatarAnimController`. However due to time constraint, there is not much functionality in this controller class, but it can serve as the base system.
`PartAttacher` could also make use of dependency injection to subscribe itself into the controller, instead of running through a `GetComponentsInChildren` in `Awake`, but this is the quickest way.

## Others
### ApparelPartsSO
Data object to store different sets of apparel, currently used in randomizer and on UI to populate buttons.
### ApparelListUI
Simple script to populate buttons and directly call AvatarController for quick access on changing apparels. Also calls CameraController for basic camera movements.
### CameraController
Make use of Cinemachine to follow main avatar. Only setup 2 cameras for a zoom in on avatar's head region, and a full body view camera. Supports basic rotation on the Y-axis to see behind of the avatar.
### AvatarSpawner
Spawn from a range of 0 to 1000 avatars on the scene via a slider, reuse avatars in a object pool.

# Optimization
On testing of roughly 500 avatars on the scene, it takes a toll on any device to render an estimate of 62000 draw calls. The only basic optimization that is performed is to enable GPU instancing, so that draw calls are batched and reduce GPU's workload. Materials of part are also not duplicated into `new Material()` and made sure that they are all using the same shared materials via `PartAttacher` script.

Since I did not implement any static meshes or props into the scene, I could not really take advantage of combining meshes. 
Material atlasing also requires usage of external software to remap the UVs of each mesh, and also it may not serve the functionality of having just a few avatars but render a big atlas on the scene that includes a lot of parts without fully utilizing them.
Basic occlusion is turned on by default, and since I don't have much obstruction in my scene, not much optimization is done here.

Hence, only GPU instancing is performed, and roughly 250+ calls are batched.
## Before
<img width="1432" height="1182" alt="Before_FrameDebugger_500" src="https://github.com/user-attachments/assets/75df84d6-63dc-4341-bf5d-5bd98847508f" />
<img width="1447" height="1199" alt="Before_Profiler_500" src="https://github.com/user-attachments/assets/56e1b4ec-c34f-449f-b6cd-c5312ac26ec2" />

## After
<img width="1429" height="1127" alt="After_FrameDebugger_500" src="https://github.com/user-attachments/assets/2b167709-c576-442e-ab2f-72e0a85b8d8c" />
<img width="1441" height="1173" alt="After_Profiler_500" src="https://github.com/user-attachments/assets/4b69eb72-8783-4bd1-868b-ddc193ab72f7" />

# Conclusion
This is a very basic system to customize an avatar. There are a lot more that can be done in an actual system apply to games, be it structural on codes, or further optimization to support low-end devices. Importing FBX models into Unity may also be tedious, and this can be helped by implementing some form of editor tool to have a better view on an editor UI, or a custom AssetImporter that can handle ScriptableObjects creation for example. Material settings can also be saved as a template and applied within AssetImporter script, instead of manually ticking settings on importing FBX model all the time.

However, since this is just a quick proof of concept or a prototype, this project successfully make necessary things work in the quickest way possible.

# Demo Video


https://github.com/user-attachments/assets/13f5e172-7429-4df7-88eb-f147a033a310



# Used Tools
Animations FBX from Mixamo
Unity Cinemachine
