using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;



#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        if(GUILayout.Button("generate")) {
            (target as Generator).Generate();
        }
    }
}
#endif

public class Generator : MonoBehaviour
{
    public GameObject chest;
    public GameObject wall;
    public GameObject floor;

    public int chestCount = 3;
    public int roomSize = 20;

    public Rect zone;

    private List<GameObject> floors = new();
    private List<GameObject> chests = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate() {
        Reset();
        GenerateChests();
    }

    private void Reset() {
        chests.ForEach(x => Destroy(x));
        chests.Clear();
    }
    private void GenerateChests() {
        for(int i = 0; i < chestCount; i++) {
            var instance = Instantiate(chest, zone.RandomInRect(), Quaternion.identity, transform);
            chests.Add(instance);
        }
    }

    private void GenerateRooms() {
        foreach(var c in chests) {
            var room = new List<GameObject>();
            var instance = Instantiate(floor, c.transform.position, Quaternion.identity, transform);
            room.Add(instance);

            for(int i = 0; i < roomSize - 1; i++) {
                var choice = room.Where(x => GetNeighbours(room, x.transform.position).Count < 4).OrderBy(x => Random.value).FirstOrDefault();
                if(!choice) throw new System.Exception("invalid generation");
                
                var availableAdjacents = GetAvailableAdjacents(room, choice.transform.position);
                
            }
        }
    }

    List<GameObject> GetNeighbours(List<GameObject> list, Vector2 position) {
        var neighs = new List<GameObject>();
        for(int i = -1; i <= 1; i++) {
            for(int j = -1; j <= 1; j++) {
                if(Mathf.Abs(i + j - 1) > 0.01f) continue; // if i + j != 1

                var pos = position + new Vector2(i, j);
                var choice = list.FirstOrDefault(x => Vector2.Distance(pos, x.transform.position) < 0.01f);
                if(!choice) continue;

                neighs.Add(choice);
            }
        }
        return neighs;
    }
    List<Vector2> GetAvailableAdjacents(List<GameObject> list, Vector2 position) {
        var neighs = new List<Vector2>();
        for(int i = -1; i <= 1; i++) {
            for(int j = -1; j <= 1; j++) {
                if(Mathf.Abs(i + j - 1) > 0.01f) continue; // if i + j != 1

                var pos = position + new Vector2(i, j);
                var choice = list.FirstOrDefault(x => Vector2.Distance(pos, x.transform.position) < 0.01f);
                if(choice) continue;

                neighs.Add(pos);
            }
        }
        return neighs;
    }
    void OnDrawGizmosSelected() {
        #if UNITY_EDITOR
        Handles.DrawSolidRectangleWithOutline(zone, Vector4.zero, Color.red);
        #endif
    }
}
