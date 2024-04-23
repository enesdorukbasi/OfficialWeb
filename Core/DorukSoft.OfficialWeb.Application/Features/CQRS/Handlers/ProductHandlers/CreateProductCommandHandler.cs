using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ProductCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly FileUploader _fileUploader;

        public CreateProductCommandHandler(IRepository<Product> productRepository, FileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product
                {
                    Title = request.Title,
                    Content = request.Content,
                    Keywords = request.Keywords,
                    Price = request.Price,
                    Tax = request.Tax,
                    Discount = request.Discount,
                    Quantity = request.Quantity,
                    ProductSalesType = request.ProductSalesType,
                    CategoryId = request.CategoryId,
                };
                var validator = new ProductValidator();
                var validateResult = validator.Validate(product);
                if (validateResult.IsValid)
                {
                    var imageUrls = new List<string>();
                    if (request.UpdatedImages != null)
                    {
                        foreach (var image in request.UpdatedImages)
                        {
                            var url = await _fileUploader.UploadFile(image);
                            imageUrls.Add(url);
                        }
                    }
                    product.ImageUrls = imageUrls;
                    var entity = await _productRepository.CreateAsync(product);
                    if (entity != null)
                    {
                        return new IDTO<object?>(200, null, ["Ürün eklendi."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, ["Ürün eklenemedi."]);
                    }
                }
                return new IDTO<object?>(300, null, validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
