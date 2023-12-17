using AutoMapper;
using firsttask.Data;
using firsttask.Models;
using Microsoft.EntityFrameworkCore;

namespace firsttask.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyFirstContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(MyFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddProductAsync(PostProductModel product)
        {
            if (!((product.CategoryId > 0) && (product.CategoryId < await _context.Products.CountAsync())))
                return -1;//return -1 if an error accures
            var tosave = _mapper.Map<Product>(product);
            var myfile = product.ImageFile;
            if (myfile != null && myfile.Length > 0)
            {
                using (var memorystream = new MemoryStream())
                {
                    myfile.CopyTo(memorystream);
                    tosave.ImageData = memorystream.ToArray();
                }
            }
            if (tosave == null)
                return -1;//return -1 if an error accures
            await _context.Products.AddAsync(tosave);
            await _context.SaveChangesAsync();
            return tosave.ProductId;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var myitem = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == id);
            if (myitem == null)
                return false;
            _context.Products.Remove(myitem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync(int page, int pagesize)
        {
            IEnumerable<Product> myproducts = await _context.Products.Include(l => l.Category).ToListAsync();
            var myreturndata = _mapper.Map<List<ProductViewModel>>(myproducts);
            foreach (var x in myreturndata)
            {
                var y = myproducts.SingleOrDefault(c => c.ProductId == x.ProductId);
                if (y.ImageData != null)
                    x.ImageBase64 = Convert.ToBase64String(y.ImageData);
            }
            var totalcount = myreturndata.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var productsperpage = myreturndata.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return productsperpage;
        }

        public async Task<ProductViewModel> GetByIdProductAsync(int id)
        {
            var myproduct = await _context.Products.Include(l => l.Category).FirstOrDefaultAsync(x => x.ProductId == id);
            var myreturndata = _mapper.Map<ProductViewModel>(myproduct);
            if (myproduct.ImageData != null)
            {
                myreturndata.ImageBase64 = Convert.ToBase64String(myproduct.ImageData);
            }
            return myreturndata;
        }

        public async Task<int> UpdateProductAsync(int id, PostProductModel product)
        {
            if (!((product.CategoryId > 0) && (product.CategoryId < await _context.Products.CountAsync())))
                return -1;//return -1 if an error accures
            var producttoedit = await _context.Products.SingleOrDefaultAsync(current => current.ProductId == id);
            if (producttoedit == null)
                return -1;//return -1 if an error accures
            var myfile = product.ImageFile;
            if (myfile != null && myfile.Length > 0)
            {
                using (var memorystream = new MemoryStream())
                {
                    await myfile.CopyToAsync(memorystream);
                    producttoedit.ImageData = memorystream.ToArray();
                }
            }
            producttoedit.ProductName = product.ProductName;
            producttoedit.ProductPrice = product.ProductPrice;
            producttoedit.CategoryId = product.CategoryId;
            producttoedit.Description = product.Description;
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
