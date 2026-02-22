using UnityEngine;

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
        
    }
    

    // Update is called once per frame

    // This is the function that will create a Plane in the scene
    void CreateGround()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 0, 0);
        plane.transform.localScale = new Vector3(SizeOfPlane, 1, SizeOfPlane);
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
        int PyramidNumber = PyramidLayers;
        GameObject pyramid = new GameObject("Pyramid");
        for (int i = 0; i <= PyramidLayers; i++)//Height
        {
                
            for (int r = 0; r <= PyramidNumber; r++)//Length
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localPosition = new Vector3(0 + r, 1 + i, 0);
                cube.transform.parent = pyramid.transform; 

                for (int j = 0; j <= PyramidNumber; j++)//Width
                    {
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.localPosition = new Vector3(0, 1 + i, 0 + j);
                        cube.transform.parent = pyramid.transform; 

                    }
              
            }
            PyramidNumber--;
        }

 //I took the x and z positions of each cube in the layer and multiplied it by the layer number * 0.5
//array to grab colors based on I?
    }
/*
 I did a variable with a range for the pyramid size 
 I used three nested for loops to build the pyramid it runs for each level of the pyramid, 
 offsetting its location each time and setting a random material color each time */

    void CreateCelestialBody()
    {
        // This function will create a celestial body (like a sun or moon) in the sky
        GameObject celestialBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialBody.transform.position = new Vector3(0, 20, 0); // Position it high in the sky
        celestialBody.transform.localScale = new Vector3(5, 5, 5); // Make it larger than a normal sphere
        celestialBody.GetComponent<Renderer>().material.color = new Color(1f, 0.9f, 0.6f); // Set the material to a bright color (like the sun)
    }


}