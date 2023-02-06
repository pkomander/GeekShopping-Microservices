using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public CartRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);

            if(cartHeader != null)
            {
                _context.CartDetails.RemoveRange(_context.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new Cart
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
            };
            cart.CartDetails = _context.CartDetails.Where(x => x.CartHeaderId == cart.CartHeader.Id).Include(x => x.Product);
            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.Id == cartDetailsId);

                int total = _context.CartDetails.Where(x => x.CartHeaderId == cartDetail.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetail);
                
                if(total == 1)
                {
                    var cartHeaderRemove = await _context.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderRemove);
                }
                _context.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            //verificar se o produto ja foi salvo na base de dados, se nao estiver iremos adicionalo
            Cart cart = _mapper.Map<Cart>(vo);
            var produt = await _context.Products.FirstOrDefaultAsync(
                x => x.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if (produt == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //verificando se o cartHeader e nulo
            //AsNoTracking informa ao Entity que nao aplicaremos mudancas
            var cartHeader = await _context.CartHeaders.AsNoTracking().
                FirstOrDefaultAsync(x => x.UserId == cart.CartHeader.UserId);

            if(cartHeader == null)
            {
                //se for nulo ira criar um novo cartheader
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                //se o header nao for nulo
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    x => x.ProductId == vo.CartDetails.FirstOrDefault().ProductId &&
                    x.CartHeaderId == cartHeader.Id);
                if (cartDetail == null)
                {
                    //criamos o cartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //atualizamos o contador do produto e cartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartVO>(cart);
        }
    }
}
