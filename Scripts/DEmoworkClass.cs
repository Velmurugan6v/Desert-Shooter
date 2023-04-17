using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEmoworkClass : MonoBehaviour
{
    private int x = 10;
    private float y = 10;
    //private string z = "VElmtu";
    //private bool w;

    // Start is called before the first frame update
    void Start()
    {
        /*Human humanOne = new Human("Velmurugan",22,"RVM");
        print(humanOne.name);
        print(humanOne.age);
        print(humanOne.native);*/

        /*Car carOne = new Car();
        carOne.Name = "Tesla";
        carOne.Price = 100000;
        carOne.forSell = true;
        print(carOne.Name);
        print(carOne.Price);
        print(carOne.forSell);
        //print(carOne.)*/

        //var lamdaDemo = (int m) => m * m;
        //var Weapon = (Name: "Pistol", Ammo: 7);

        //print(Weapon.);
    }

    

    int AddScore()
    {
        int z = x + (int)y;
        return z;
    }
}

#region Constructor
public class Human
{

    public string name;     //field or Variable
    public int age;         //field or Variable
    public string native;   //field or Variable

    public Human(string _name,int _age,string _native)         //Create a Contructor
    {
        name = _name;
        age = _age;
        native = _native;
    }
}
#endregion

#region Properties
public class Car
{
    private string name;   //create a field or Variables

    public string Name    //This is a process to Properties 
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public int Price { get; set; }  //short line of Propertie
    public bool forSell;
}

#endregion
