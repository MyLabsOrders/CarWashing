using CarWashing.Infrastructure.Services.DatabaseServices;

using var generator = new DatabaseGenerationService();
generator.Generate();
