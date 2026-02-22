using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class CreateScene : MonoBehaviour
{
    public int SizeOfForest;
    public int NumberOfTrees;

    [Range(3, 10)]
    public int PyramidLayers;
    
    public int SizeOfPlane;
    //public Gameobject[] trees;
    //public Gameobject[] stones;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateGround();
        CreateForest();
        CreatePyramid();
        CreateCelestialBody();
    }

    void Update()
    {
        // Find the celestial body in the scene
        GameObject celestialBodyHolder = GameObject.Find("CelestialBody");
        // Rotate the celestial body to simulate movement across the sky
        celestialBodyHolder.transform.Rotate(new Vector3(1, 0, 0), 30 * Time.deltaTime); // Rotate around the x-axis to simulate movement 
    }
    

    // Update is called once per frame

    // This is the function that will create a Plane in the scene
    void CreateGround()
    {
        // Create an empty GameObject to hold the plane
        GameObject ground = new GameObject("Ground");       // This creates an empty GameObject named "Ground" which will be used as a parent for the plane, helping to keep the hierarchy organized
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);     // This creates a plane primitive which will serve as the ground in our scene
        plane.transform.parent = ground.transform;      // This sets the parent of the plane to the ground GameObject, so it will be organized under the ground in the hierarchy
        plane.transform.position = new Vector3(0, 0, 0);        // This sets the position of the plane to the origin (0, 0, 0) in the scene, so it will be centered at the world origin
        plane.transform.localScale = new Vector3(SizeOfPlane, 1, SizeOfPlane);      // This scales the plane to make it larger, using the SizeOfPlane variable for the x and z scale, while keeping the y scale at 1 since we don't want to stretch it vertically
        // Set the material of the plane to a Sandy Beige color
        plane.GetComponent<Renderer>().material.color = new Color(0.8f, 0.7f, 0.5f);
    }

    void CreateForest()
    {
        // Create an empty GameObject to hold all the trees in the forest
        GameObject forest = new GameObject("Forest");       // This creates an empty GameObject named "Forest" which will be used as a parent for all the tree objects we create, helping to keep the hierarchy organized
        forest.transform.position = new Vector3(-25, 0, 25);     // This sets the position of the forest GameObject to the origin (-25, 0, 25) in the scene. This means that all trees will be positioned relative to this point, making it easier to manage their placement within the forest.
        for (int t = 0; t < NumberOfTrees; t++)     // This loop will run for the number of trees we want to create, allowing us to generate multiple trees in the forest
        {
            float x = Random.Range(-SizeOfForest, SizeOfForest);        // Random.Range is a function that returns a random float number between the two values you provide
            float z = Random.Range(-SizeOfForest, SizeOfForest);        // This will give us a random position for the tree within the specified range
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);       // This creates a cylinder primitive which we will use as a tree trunk
            tree.transform.parent = forest.transform;        // This sets the parent of the tree to the forest GameObject, so all trees will be organized under the forest in the hierarchy
            tree.transform.localPosition = new Vector3(x, 0, z);       // This sets the position of the tree to the random x and z values we generated, with a y value of 0 so it sits on the ground
            int RandomHeight = Random.Range(1, 5);       // This generates a random height for the tree between 1 and 5
            int RandomWidth = Random.Range(1, 3);        // This generates a random width for the tree between 1 and 3
            tree.transform.localScale = new Vector3(RandomWidth, RandomHeight, RandomWidth);        // This sets the scale of the tree to the random width and height we generated, with the same width for x and z to keep it cylindrical
            tree.GetComponent<Renderer>().material.color = new Color(0.5f, 0.3f, 0.1f);// Set the material of the tree to a Brown color
        }
    }


     void CreatePyramid()
    {
        
        #region Colors 
        Dictionary<int, Color> layerColors = new Dictionary<int, Color>()
    {
        { 0, Color.red },
        { 1, Color.orange },
        { 2, Color.yellow },
        { 3, Color.limeGreen },
        { 4, Color.green },
        { 5, Color.cyan },
        { 6, Color.blue },
        { 7, Color.indigo },
        { 8, Color.purple }, // Orange
        { 9, Color.magenta } // Purple
    };
        #endregion

        float spacing = 1.2f; //adds the gap
        int PyramidNumber = PyramidLayers;
        GameObject pyramid = new GameObject("Pyramid");
        pyramid.transform.position = new Vector3(15, .8f, 0);
        for (int i = 0; i <= PyramidLayers; i++)//Height
        {
                float offset = i * 0.6f;

            for (int r = 0; r < PyramidNumber; r++)//Rows
            {
                for (int j = 0; j < PyramidNumber; j++)//Columns
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.parent = pyramid.transform;

                        // Combine j, i, and r into one position
                        // We add 'offset' to X and Z so it stays centered
                        float x = (j * spacing) + offset;
                        float y = i * (spacing - 0.1f); // Set the y position based on the layer number
                        float z = (r * spacing) + offset;

                        cube.transform.localPosition = new Vector3(x, y, z);
                        cube.GetComponent<Renderer>().material.color = layerColors[i]; // Set the material color based on the layer number

                    }
              
            }
            PyramidNumber--;
        }

    

    }

    void CreateCelestialBody()
    {
        // Create an empty GameObject to hold the celestial body
        GameObject celestialBodyHolder = new GameObject("CelestialBody");
        celestialBodyHolder.transform.position = new Vector3(0, 0, 0);     // Position the holder at the origin so the celestial body will be positioned relative to it
        // This function will create a celestial body (like a sun or moon) in the sky
        GameObject celestialBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialBody.transform.position = new Vector3(0, 50, 0);       // Position it high in the sky
        celestialBody.transform.localScale = new Vector3(5, 5, 5);      // Make it larger than a normal sphere
        celestialBody.GetComponent<Renderer>().material.color = new Color(1f, 0.9f, 0.6f); // Set the material to a bright color (like the sun)
        celestialBody.transform.parent = celestialBodyHolder.transform;         // Set the parent of the celestial body to the holder for better organization
        // Add a Directional light to the celestial body to make it emit light
        Light light = celestialBody.AddComponent<Light>();
        light.type = LightType.Directional;       // Set the light type to directional, which
        light.color = new Color(1f, 0.9f, 0.6f); // Set the light color to match the celestial body
        light.intensity = 1.5f;      // Adjust the intensity of the light
        light.transform.rotation = Quaternion.Euler(90, 0, 0); // Rotate the light to shine down on the scene at an angle
    }


}