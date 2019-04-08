using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    public class BufferCategories
    {
        List<BufferCategory> _Categories;

        public BufferCategories()
        {

            _Categories = new List<BufferCategory>
            {
                { new BufferCategory(1, 0.1, 5.4, 1, 0.1, 0.1) },
                { new BufferCategory(2, 5.5, 12.4, 2, 0.1, 0.2) },
                { new BufferCategory(3, 12.5, 20.4, 3, 0.1, 0.3) },
                { new BufferCategory(4, 20.5, 28.0, 4, 0.1, 0.4) }
            };
        }
        
        public BufferCategory GetCategory(int CategoryD)
        {
            try
            {
                foreach (BufferCategory cls in _Categories)
                {
                    if (cls.Category == CategoryD)
                        return cls;
                }
                return MaximumCategory();
            }

            catch (Exception) { throw; }            
        }

        public int MaximumCategoryID()
        {
            return _Categories.Max(t => t.Category);
        }
        public int MinimumCategoryID()
        {
            return _Categories.Min(t => t.Category);
        }
        public BufferCategory MaximumCategory()
        {
            return _Categories.Find(t => t.Category == MaximumCategoryID());
        }
        public BufferCategory MinimumCategory()
        {
            return _Categories.Find(t => t.Category == MinimumCategoryID());
        }
    }
}
