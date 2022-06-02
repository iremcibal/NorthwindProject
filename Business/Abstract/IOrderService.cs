using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{

    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int orderId);
        IResult Add(Order order);

        //IResult Update(Product product);
        //IResult Delete(int productId);
    }
}
