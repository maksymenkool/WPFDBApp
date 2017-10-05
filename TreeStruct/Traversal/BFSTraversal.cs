using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStruct
{
    public class BFSTraversal<T> : ITreeWalker<T>
    {
        public IEnumerable<TreeNode<T>> Walk(TreeNode<T> tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(
                "Cannot traversal null value!");
            }
            var q = new Queue<TreeNode<T>>();
            q.Enqueue(tree);
            while (q.Count > 0)
            {
                TreeNode<T> current = q.Dequeue();
                yield return current;
                foreach (var child in current.Children)
                    q.Enqueue(child);
            }
        }
    }
}
