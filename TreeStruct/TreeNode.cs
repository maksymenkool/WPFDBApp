using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStruct
{
    /// <summary>
    /// A <code>Tree</code> is a general-purpose node in a tree data structure.
    /// 
    /// A tree node may have at most one parent and 0 or more children.
    /// A node's tree is the set of all nodes that can be reached by starting
    /// at the node and following all the possible links to parents and children.
    /// A node with no parent is the root of its tree; a node with no children is a leaf.
    /// A tree may consist of many subtrees, each node acting as the root for its own subtree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {
        /*private T data;
        private TreeNode<T> parent;
        private bool hasParent;
        private List<TreeNode<T>> children;*/

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode(T data, params TreeNode<T>[] children)
            : this(data)
        {
            foreach (var child in children)
            {
                this.AddChild(child);
            }
        }

        public T Data { get; private set; }

        public TreeNode<T> Parent { get; private set; }

        public bool HasParent { get; private set; }

        public List<TreeNode<T>> Children { get; private set; }

        /*public TreeNode<T> Root
        {
            get
            {
                return (Parent == null) ? this : Parent.Root;
            }
        }*/

        /// <summary>
        /// Returns true if this tree is empty.
        /// </summary>
        /// <returns>true|false</returns>
        public bool IsRoot
        {
            get { return this.Parent == null; }
        }

        public Boolean IsLeaf
        {
            get { return this.Children.Count == 0; }
        }

        public int Level()
        {
            if (this.IsRoot) return 0;
            return Parent.Level() + 1;
        }

        public int Height
        {
            get
            {
                return Depth(this);
            }
        }

        private int Depth(TreeNode<T> node)
        {
            if (node == null)
                return 0;
            else
            {
                int maxDepth = 0;
                foreach (var child in node.Children)
                {
                    maxDepth = Math.Max(maxDepth, Depth(child));
                }
                return maxDepth + 1;
            }
        }

        /// <summary>
        /// Adds the child to the end of this node's child list.
        /// Sets the child's parent to this node.
        /// </summary>
        /// <param name="childData">Input data.</param>
        public TreeNode<T> AddChild(TreeNode<T> childNode)
        {
            if (childNode == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }

            if (childNode == this)
            {
                throw new ArgumentException(
                "Cannot insert this value!");
            }

            if (childNode.HasParent)
            {
                throw new ArgumentException(
                "The node already has a parent!");
            }



            /*if (childNode == this.Root)
            {
                throw new ArgumentException(
                "Cannot insert this value!");
            }*/

            childNode.Parent = this;
            childNode.HasParent = true;
            this.Children.Add(childNode);
            return childNode;
        }

        public TreeNode<T> GetChild(int index)
        {
            return Children[index];
        }

        /// <summary>
        /// Deletes the child from the node's child list.
        /// </summary>
        /// <param name="child">A Node for deletions.</param>
        /// <returns>true|false</returns>
        public bool DeleteChild(TreeNode<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(
                "Cannot delete null value!");
            }
            return this.Children.Remove(child);
        }

        /// <summary>
        /// Deletes the child from the the specified position in this node's child list.
        /// </summary>
        /// <param name="index">The specified position in this node's child list.</param>
        /// <returns>true|false</returns>
        public bool DeleteChildAtIndex(int index)
        {
            if (index < 0 || index > this.Children.Count)
            {
                throw new IndexOutOfRangeException(
                    "Cannot delete child node with this index.\n" +
                    "The index out of range.");
            }
            if (index < this.Children.Count)
            {
                this.Children.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes the child list of node's.
        /// </summary>
        public void DeleteChildren()
        {
            this.Children = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Returns the number of nodes.
        /// </summary>
        /// <returns>Int</returns>
        public int TreeSize
        {
            get
            {
                int numberOfNodes = 0;
                if (this != null)
                {
                    numberOfNodes = NodesCount(this) + 1;
                }
                return numberOfNodes;
            }
        }

        private int NodesCount(TreeNode<T> node)
        {
            int numberOfNodes = node.Children.Count;
            foreach (var child in node.Children)
            {
                numberOfNodes += NodesCount(child);
            }
            return numberOfNodes;
        }

        /// <summary>
        /// Returns the number of children of this node.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int ChildrenCount()
        {
            return Children.Count;
        }

        /// <summary>
        /// Returns true if the input data is in this tree.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true | false</returns>
        public bool IsDataInTree(T data)
        {
            return (GetNodeWithData(data) != null);
        }

        /// <summary>
        /// Returns node if the input data is in this tree.
        /// </summary>
        /// <param name="data">Itput data.</param>
        /// <returns>TreeNode | null</returns>
        public TreeNode<T> GetNodeWithData(T data)
        {
            return FindData(this, data);
        }

        private TreeNode<T> FindData(TreeNode<T> node, T data)
        {
            TreeNode<T> result = null;
            if (node.Data.Equals(data))
            {
                return result = node;
            }
            else
            {
                foreach (var child in node.Children)
                {
                    result = FindData(child, data);
                    if (null != result) break;
                }
            }
            return result;
        }

        public TreeNode<T> FindNodeTraversalTree(TreeNode<T> tree, int traversal, T findData)
        {
            BFSTraversal<T> bfs = new BFSTraversal<T>();
            DFSTraversal<T> dfs = new DFSTraversal<T>();
            IEnumerable<TreeNode<T>> traversalNode = (traversal == 1) ? dfs.Walk(tree) : bfs.Walk(tree);

            TreeNode<T> findNode = null;
            foreach(var node in traversalNode)
            {
                if(node.Data.Equals(findData))
                {
                    findNode = node;
                    break;
                }
            }
            return findNode;
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            yield return this;
            foreach (var child in this.Children)
            {
                foreach (var anychild in child)
                    yield return anychild;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
