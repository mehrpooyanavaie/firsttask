using AutoMapper;
using firsttask.Data;
using firsttask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace firsttask.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyFirstContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(MyFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddCategoryAsync(PostCategoryModel category)
        {
            var tosave = _mapper.Map<Category>(category);
            if (tosave == null)
                return -1;//return -1 if an error accures
            await _context.Categories.AddAsync(tosave);
            await _context.SaveChangesAsync();
            return tosave.CategoryId;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var mycategory = await _context.Categories.FindAsync(id);
            if (mycategory == null)
                return false;
            if (!mycategory.Products.IsNullOrEmpty())//in database cascade
                return false;
            _context.Categories.Remove(mycategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync(int page, int pagesize)
        {
            IEnumerable<Category> categories = await _context.Categories.Include(l => l.Products).ToListAsync();
            var myreturndata = _mapper.Map<List<CategoryViewModel>>(categories);
            var totalcount = myreturndata.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var categoriesperpage = myreturndata.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return categoriesperpage;
        }
        public async Task<CategoryViewModel> GetByIdCategoryAsync(int id)
        {
            var mycategory = await _context.Categories.Include(l => l.Products).SingleOrDefaultAsync(x => x.CategoryId == id);
            var myreturndata = _mapper.Map<CategoryViewModel>(mycategory);
            return myreturndata;
        }

        public async Task<int> UpdateCategoryAsync(int id, PostCategoryModel category)
        {
            var categorytoedit = await _context.Categories.SingleOrDefaultAsync(current => current.CategoryId == id);
            if (categorytoedit == null)
                return -1;
            categorytoedit.CategoryName = category.CategoryName;
            return id;
        }
    }
}
