# Unity Tools
## Normalize Meshes
The [MeshNormalizer](https://github.com/Toorah/Unity-Tools/blob/master/Assets/com.toorah.meshnormalizer/Editor/MeshNormalizer.cs) script makes sure that any objects with a negative scale are set to scale (1, 1, 1) but keeps their shape.
This is useful for certain operations which do not support negative scale.
Colliders sometimes also complain about negative scale and this could help solve the issue.
### How it works
- The script first stores the world positions of the vertices of the mesh.
- It then creates and assigns a copy of the mesh.
- Next it resets [transform.localScale](https://docs.unity3d.com/ScriptReference/Transform-localScale.html "Unity Scripting Reference").
- Lastly, it overwrites the vertex positions and inverts the [Mesh.triangles](https://docs.unity3d.com/ScriptReference/Mesh-triangles.html "Unity Scripting Reference") array (to flip the normals).

### How to Use
Apply the script to a GameObject with negative scale.
Click the "Normalize" Button.
Done!
(You can now remove the script again)

### Todo
- Add [Undo](https://docs.unity3d.com/ScriptReference/Undo.html "Unity Scripting Reference") Support
- Make it an Editor Window instead of a Component
