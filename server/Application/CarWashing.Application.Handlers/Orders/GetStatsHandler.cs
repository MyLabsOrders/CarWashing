using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using static CarWashing.Application.Contracts.Orders.Queries.GetStats;

namespace CarWashing.Application.Handlers.Orders;

internal class GetStatsHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;

    public GetStatsHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        if ((await _context.Orders
            .Where(order => request.From.ToUniversalTime() <= order.OrderDate && order.OrderDate <= request.To.ToUniversalTime())
            .ToListAsync(cancellationToken)).Any())
        {
            var stats = (await _context.Orders
            .Where(order => request.From.ToUniversalTime() <= order.OrderDate && order.OrderDate <= request.To.ToUniversalTime())
            .ToListAsync(cancellationToken)).GroupBy(order => order.Name).Select(orderGroup => new
            {
                Name = orderGroup.Key,
                Value = orderGroup.Sum(order => order.Amount)
            });
            return new Response(new MemoryStream(new ChromePdfRenderer
            {
                RenderingOptions = new ChromePdfRenderOptions
                {
                    EnableJavaScript = true,
                    CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print
                }
            }.RenderHtmlAsPdf(new StringBuilder(File.ReadAllText("Assets/Chart.html"))
                .Replace("{Statistics}", string.Join(
                    ",",
                    stats.Select(s => $"{{name:'{s.Name}',value:{s.Value}}}")
                ))
                .ToString()).BinaryData));
        }

        throw new EntityNotFoundException("Orders within this date range were not found");
    }
}
