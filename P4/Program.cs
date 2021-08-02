using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje4
{
    class Graph
    {
        public int _V;
        public LinkedList<int>[] _adj;

        public Graph(int V)
        {
            _adj = new LinkedList<int>[V];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<int>();
            }
            _V = V;
        }
        public void AddEdge(int v, int w)
        {
            _adj[v].AddLast(w);

        }
        public void BFS(int s)
        {
            bool[] visited = new bool[_V];
            for (int i = 0; i < _V; i++)
                visited[i] = false;

            LinkedList<int> queue = new LinkedList<int>();
            visited[s] = true;

            queue.AddLast(s);

            while (queue.Any())
            {
                s = queue.First();
                Console.Write(s + " ");
                queue.RemoveFirst();

                LinkedList<int> list = _adj[s];

                foreach (var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
        }
    }
    class MST
    {
        public int V = 5;
        public int minKey(int[] key, bool[] mstSet)
        {

            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }
        public void printMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("Kenar \tWeight ");
            for (int i = 1; i < V; i++)
                Console.WriteLine(parent[i] + " - " + i + "\t" + graph[i, parent[i]]);
        }
        public void primMST(int[,] graph)
        {
            int[] parent = new int[V];
            int[] key = new int[V];
            bool[] mstSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < V - 1; count++)
            {
                int u = minKey(key, mstSet);
                mstSet[u] = true; 
                for (int v = 0; v < V; v++)
                    if (graph[u, v] != 0 && mstSet[v] == false
                        && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }
            printMST(parent, graph);
        }
    }
    class GFG
    {
        int V = 9;
        int minDistance(int[] dist,
                        bool[] sptSet)
        {

            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        void printSolution(int[] dist)
        {
            Console.Write("Vertex \t\t Kaynaktan "
                        + "Uzaklık\n");
            for (int i = 0; i < V; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        public void dijkstra(int[,] graph, int src)
        {
            int[] dist = new int[V];

            bool[] sptSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            dist[src] = 0;

            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet);
                sptSet[u] = true;
                for (int v = 0; v < V; v++)
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }
            printSolution(dist);
        }
    }
    public class Node
    {
        public int key, height;
        public Node left, right;

        public Node(int d)
        {
            key = d;
            height = 1;
        }
    }
    public class AVLTree
    {

        public Node root;

        // Ağacın yüksekliğini bulmak için kullandığımız fonksiyon
        public int height(Node N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        // İki sayının maksimumunu bulma fonksiyonu
        public int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // Kökü y olan ağacı sağa döndürme fonksiyonu
        public Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            
            x.right = y;
            y.left = T2;

            // Yükseklikleri güncelliyoruz
            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            // Yeni rootu returnledik.
            return x;
        }

        //Kökü x olan ağacı sola döndürme fonksiyonu
        public Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;
     
            y.left = x;
            x.right = T2;

            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            return y;
        }

        // Balance faktörü alma fonksiyonu
        public int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        public Node insert(Node node, int key)
        {

            // BST'ye normal bir şekilde yerleştirme yapıyor.
            if (node == null)
                return (new Node(key));

            if (key < node.key)
                node.left = insert(node.left, key);
            else if (key > node.key)
                node.right = insert(node.right, key);
            else // Yinelenen keylere izin vermiyor. 
                return node;

            // Ata node'nin yüksekliği güncelleniyor.
            node.height = 1 + max(height(node.left),
                                height(node.right));

            int balance = getBalance(node);

            if (balance > 1 && key < node.left.key)
                return rightRotate(node);
            
            if (balance < -1 && key > node.right.key)
                return leftRotate(node);
           
            if (balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            if (balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }
        public void preOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.key + " ");
                preOrder(node.left);
                preOrder(node.right);
            }
        }
    }
    class Program
    {
        public static void Main(String[] args)
        {
            AVLTree tree = new AVLTree();

            tree.root = tree.insert(tree.root, 10);
            tree.root = tree.insert(tree.root, 20);
            tree.root = tree.insert(tree.root, 30);
            tree.root = tree.insert(tree.root, 40);
            tree.root = tree.insert(tree.root, 50);
            tree.root = tree.insert(tree.root, 25);
            Console.Write("Preorder : ");
            tree.preOrder(tree.root);

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------");

            int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                    { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                    { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                    { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                    { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                    { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                    { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                    { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                    { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
            GFG t = new GFG();
            t.dijkstra(graph, 0);

            Console.WriteLine("--------------------------------------------------------------");

            int[,] graph2 = new int[,] { { 0, 2, 0, 6, 0 },
                                      { 2, 0, 3, 8, 5 },
                                      { 0, 3, 0, 0, 7 },
                                      { 6, 8, 0, 0, 9 },
                                      { 0, 5, 7, 9, 0 } };

            MST sT = new MST();
            sT.primMST(graph2);

            Console.WriteLine("--------------------------------------------------------------");

            Graph g = new Graph(4);

            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 3);

            Console.Write("Vertex2'den Başlayarak: ");
            g.BFS(2);

            Console.ReadKey();
        }
    }
}
