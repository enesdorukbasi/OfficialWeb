using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ProductCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly FileUploader _fileUploader;
        public UpdateProductCommandHandler(IRepository<Product> productRepository, FileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _productRepository.GetByIdAsync(request.ProductId);
                if (entity != null)
                {
                    if (request.DeletedImageUrls != null && request.DeletedImageUrls.Count > 0)
                    {
                        foreach (var deletedUrl in request.DeletedImageUrls)
                        {
                            _fileUploader.DeleteFile(deletedUrl);
                            if (entity.ImageUrls != null)
                            {
                                entity.ImageUrls.Remove(deletedUrl);
                            }
                        }
                    }
                    if (request.ChangedImages != null && request.ChangedImages.Count > 0)
                    {
                        foreach (var imageUrl in request.ChangedImages)
                        {
                            var url = await _fileUploader.UploadFile(imageUrl);
                            if (entity.ImageUrls != null)
                            {
                                entity.ImageUrls.Add(url);
                            }
                        }
                    }
                    entity.Title = request.Title;
                    entity.Content = request.Content;
                    entity.Keywords = request.Keywords;
                    entity.Price = request.Price;
                    entity.Tax = request.Tax;
                    entity.Discount = request.Discount;
                    entity.Quantity = request.Quantity;
                    entity.ProductSalesType = request.ProductSalesType;
                    entity.CategoryId = request.CategoryId;
                    var validator = new ProductValidator();
                    var validationResult = validator.Validate(entity);
                    if (validationResult.IsValid)
                    {
                        var updatedEntity = await _productRepository.UpdateAsync(entity);
                        return new IDTO<object?>(200, null, ["Güncelleme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Ürün bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
