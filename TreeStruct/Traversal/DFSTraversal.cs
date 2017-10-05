using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStruct
{
    public class DFSTraversal<T> : ITreeWalker<T>
    {
        public IEnumerable<TreeNode<T>> Walk(TreeNode<T> tree)
        {
            if ( tree == null)
            {
                throw new ArgumentNullException(
                "Cannot traversal null value!");
            }
            yield return tree;
            foreach (var child in tree.Children)
            {
                foreach (var anychild in child)
                    yield return anychild;
            }
        }
    }
}
