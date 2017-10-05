using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStruct
{
    public interface ITreeWalker<T>
    {
        IEnumerable<TreeNode<T>> Walk(TreeNode<T> tree);
    }
}
