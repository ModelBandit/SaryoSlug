using UnityEngine;
using System.Collections.Generic;
using Spine.Unity;
using System.Collections;

public class OutlineRenderer : MonoBehaviour
{
    [SerializeField] MeshRenderer m_meshRenderer;
    [SerializeField] MeshFilter m_meshFilter;

    [SerializeField] MeshFilter TargetMesh;
    [SerializeField] public int MeshIndex = 0;

    public SkeletonAnimation anim;

    Dictionary<Vector2, int> m_vertexPairs;
    // Start is called before the first frame update
    void Start()
    {
        m_vertexPairs = new Dictionary<Vector2, int>();

        if (m_meshFilter == null)
            m_meshFilter = gameObject.AddComponent<MeshFilter>();
        if (m_meshRenderer == null)
            m_meshRenderer = gameObject.AddComponent<MeshRenderer>();
        if (TargetMesh == null)
        {
            Debug.LogError("Target Object is Empty");
        }
        //StartCoroutine(UpdateVertex());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MeshMaker()
    {
        Mesh targetMesh = TargetMesh.sharedMesh;

        Vector3[] vertices = TargetMesh.sharedMesh.vertices;

        int[] VertexIndex = targetMesh.triangles;
        int triangleLength = targetMesh.triangles.Length;

        GameObject pObj;
        pObj = new GameObject($"MeshObject");
        pObj.transform.parent = transform;

        for (int i = 0; i < triangleLength; i+=3)
        {
            string s;
            if (i <= 0)
                s = $"position_{i}";
            else
                s = $"position_{i / 3}";
            GameObject go = new GameObject(s);
            MeshFilter filter = go.AddComponent<MeshFilter>();
            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            go.transform.parent = pObj.transform;

            Vector3[] v3Arr = new Vector3[3];
            int[] triangleArr = new int[3];

            v3Arr[0] = vertices[VertexIndex[i]];
            v3Arr[1] = vertices[VertexIndex[i+1]];
            v3Arr[2] = vertices[VertexIndex[i+2]];
            for (int j = 0;j<3;++j)
            {
                triangleArr[j] = j;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = v3Arr;
            mesh.triangles = triangleArr;
            
            filter.sharedMesh = mesh;
        }
    }

    public void VertexMaker()
    {
        Mesh targetMesh = TargetMesh.sharedMesh;

        Vector3[] v3Arr = targetMesh.vertices;


        GameObject pObj;
        pObj = new GameObject($"Vertex");
        pObj.transform.parent = transform;

        GameObject go;
        for (int i = 0; i < v3Arr.Length; ++i)
        {
            go = new GameObject($"position_{i}");
            go.transform.position = v3Arr[i];
            go.transform.parent = pObj.transform;
        }
    }

    private List<Vector3> Extentions = new List<Vector3>();
    [SerializeField]
    [Range(0.1f, 1.0f)]
    public float Thickness;
    public Material mat;

//    //���� ��� ����
//    public void ExtentionMeshes()
//    {
//        ButterConstData data = new ButterConstData();
        
//        //������Ʈ �� ���ؽ�����Ʈ
//        List<List<Vector3>> v2dList = new List<List<Vector3>>();

//        Vector3[] v3Arr = TargetMesh.sharedMesh.vertices;

//        //������Ʈ �� ���ؽ� ����Ʈ ����
//        for(int i = 0; i < data.Count; ++i)
//        {
//            List<Vector3> v2List = new List<Vector3>();
//            for (int j = data[i-1]; j < data[i]; ++j)
//            {
//                v2List.Add(v3Arr[j]);
//            }
//            v2dList.Add(v2List);
//        }

//        //��� �߽����� �߽�
//        Vector3 mainPivot = Vector3.zero;
//        //�̹����� �߽���
//        Vector3[] pivot = new Vector3[data.Count];

//        //�� ������Ʈ�� ���ؽ��� Ȱ��. �߽��� ���
//        for(int i = 0; i < v2dList.Count; ++i)
//        {
//            pivot[i] = Vector3.zero;
//            Vector3[] vertexes = v2dList[i].ToArray();
//            for (int j = 0; j < vertexes.Length; ++j)
//            {
//                pivot[i] += vertexes[j];
//            }
//            pivot[i] = new Vector3(pivot[i].x / vertexes.Length, pivot[i].y / vertexes.Length);
//            mainPivot += pivot[i];
//        }
//        mainPivot = new Vector3(mainPivot.x / pivot.Length, mainPivot.y / pivot.Length);
//        //for (int i = 0; i < v2dList.Count; ++i)
//        //{
//        //    Debug.Log(v2dList[i].Count);
//        //}

//        ///TODO1: �߽����� �����Ͽ� ������ ������ �ĳ��� - ����: ������ ������� �ε����� �籸�� �ؾ���

//        //Option1: �ε����� ����� �ұ�Ģ����. ������ �ε������� �ƴ�
//        //�ε����� �ε��� ��ȣ�� �����Ͽ� ���������� ��

//        //Option2: ���ؽ� �ε��� ���� ���Ұ�.

//        //RESULT: �ϴ� �׳� ����� Ű���� ������

//        //TODO2: �� obj ���� �Ǻ����� n�� �о�� �޽� ����
//        //

//        int index = 0;
//        for (int i = 0; index < v3Arr.Length; ++i, ++index)
//        {
//            for (int j = 0; (index < v3Arr.Length) && (j < v2dList[i].Count); ++j, ++index)
//            {
//                Vector3 direction = (v2dList[i][j] - pivot[i]).normalized;
//                Vector3 Extention = direction * Thickness;
//                v3Arr[index] += Extention;
//                Extentions.Add(Extention);
//            }
//        }



//        GameObject go = new GameObject($"position");
//        MeshFilter filter = go.AddComponent<MeshFilter>();
//        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
//        go.transform.parent = transform;

//        PairingMesh.vertices = v3Arr;
//        PairingMesh.triangles = TargetMesh.sharedMesh.triangles;
//        filter.sharedMesh = PairingMesh;
//        renderer.material = mat;


//        //?
//        //PairingMesh.RecalculateNormals();
//        //PairingMesh.RecalculateBounds();
//    }

//    public void MakeOutline()
//    {
//        //TargetMesh.sharedMesh.sub;
//    }
//    Mesh PairingMesh;
//    private IEnumerator UpdateVertex()
//    {
//        while(true)
//        {
//            PairingMesh.vertices = TargetMesh.sharedMesh.vertices;
//            PairingMesh.triangles = TargetMesh.sharedMesh.triangles;
//            for (int i = 0; i < Extentions.Count; ++i)
//            {
//                PairingMesh.vertices[i] = TargetMesh.sharedMesh.vertices[i] + Extentions[i];
//                yield return null;
//            }
//            yield return null;
//        }
//    }

//    public void VectorCalcu()
//    {
//        Vector3 origin = new Vector3(-1, 1, 0);
//        Vector3 target = new Vector3(1, -1, 0);
        
//        Debug.Log(Vector3.Dot(target, origin));

//        /*
//using System.IO;
//        StreamWriter sr = new StreamWriter("./index.txt");
//        for (int i = 0; i < TargetMesh.sharedMesh.triangles.Length; ++i)
//        {
//            sr.WriteLine(TargetMesh.sharedMesh.triangles[i].ToString());
//        }
//        sr.Close();
//        */
//    }
}
