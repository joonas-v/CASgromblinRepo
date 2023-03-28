using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenController : MonoBehaviour
{
    //NOTES:
    //first and second child of a room are always the first and second door

    //initialize vars and stuff
    public GameObject spawnRoom;
    public GameObject lvl1Room;
    public GameObject lvl2Room;
    public GameObject bossRoom;
    public GameObject portalRooms;
    public static int nrOfRooms = 30;
    private Vector3 nextRoomPos;
    private GameObject[] rooms = new GameObject[nrOfRooms];
    public int currentRoom = 0;
    public bool roomObjectiveDone = false;
    private SpawnerController spawner;
    private bool firstTimeFloor2 = true;
    public RoomController roomScript;
    public int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        nextRoomPos = gameObject.transform.position;
        //generate spawn room
        rooms[0] = Instantiate(spawnRoom, nextRoomPos, spawnRoom.transform.rotation);
        nextRoomPos.z += 60;
        //generate rooms
        for (int i = 1; i < 5; i++)
        {
            GenerateRoom(i);
            rooms[i].transform.GetChild(0).gameObject.SetActive(false);
            rooms[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        //generate BOSS room
        rooms[5] = Instantiate(bossRoom, nextRoomPos, bossRoom.transform.rotation);
        roomScript = rooms[5].GetComponent<RoomController>();
        roomScript.ID = 5;
        nextRoomPos.z += 60;
        rooms[5].transform.GetChild(0).gameObject.SetActive(false);
        rooms[5].transform.GetChild(1).gameObject.SetActive(true);
        //generate portal rooms
        rooms[6] = Instantiate(portalRooms, nextRoomPos, portalRooms.transform.rotation);
        roomScript = rooms[6].GetComponent<RoomController>();
        roomScript.ID = 6;
        rooms[6].transform.GetChild(0).gameObject.SetActive(false);
        rooms[6].transform.GetChild(1).gameObject.SetActive(true);//WARNING: this is horrible, do not do this ever (but I am low on time)
        //just generate the entirety of lvl2 at the start bc lmao why not
        GenerateRoomsLvl2();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCheck();
        if(roomObjectiveDone)
        {
            if (rooms[currentRoom].transform.Find("FinishButton"))
            {
                Material buttonMat;
                buttonMat = rooms[currentRoom].transform.Find("FinishButton").GetComponent<Renderer>().material;
                buttonMat.color = Color.green;
            }
        }
    }

    void GenerateRoom(int roomID)
    {
        rooms[roomID] = Instantiate(lvl1Room, nextRoomPos, lvl1Room.transform.rotation);
        nextRoomPos.z += 60;
        roomScript = rooms[roomID].GetComponent<RoomController>();
        roomScript.ID = roomID;
    }

    public void GenerateRoomsLvl2()
    {
        if (firstTimeFloor2)
        {
            nextRoomPos.y += 40;
            nextRoomPos.z += 60;
        }
        for (int i = 7; i < 12; i++)
        {
            rooms[i] = Instantiate(lvl2Room, nextRoomPos, lvl2Room.transform.rotation);
            nextRoomPos.z += 60;
            RoomController roomScript;
            roomScript = rooms[i].GetComponent<RoomController>();
            roomScript.ID = i;
            rooms[i].transform.GetChild(0).gameObject.SetActive(false);
            rooms[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        //generate BOSS room
        rooms[12] = Instantiate(bossRoom, nextRoomPos, bossRoom.transform.rotation);
        nextRoomPos.z += 60;
    }

    public void RoomFinish(int roomID)
    {
        rooms[roomID].transform.GetChild(1).gameObject.SetActive(false);
        rooms[roomID + 1].transform.GetChild(0).gameObject.SetActive(false);
    }
    public void RoomStart(int roomID)
    {
        roomObjectiveDone = false;
        rooms[roomID].transform.GetChild(0).gameObject.SetActive(true);
        currentRoom += 1;
        //failsafe in case a room contains no enemy spawners
        try
        {
            if (spawner = rooms[roomID].GetComponentInChildren<SpawnerController>())
            {
                for (int i = 0; i < spawner.nrOfSpawns; i++)
                {
                    spawner.SpawnEnemy(spawner.spawns[i]);
                    enemiesRemaining = i + 1;
                }
            }
        }
        catch
        {
            print("I have met with a terrible fate"); //you fucked up something in the code
        }
    }

    public void EnemyCheck()
    {
        if(enemiesRemaining > 0)
        {
            roomObjectiveDone = false;
        }
        else
        {
            roomObjectiveDone = true;
        }
    }
}

