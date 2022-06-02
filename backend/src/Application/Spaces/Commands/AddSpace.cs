using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Spaces.Entities;
using Domain.Spaces.Repositories;
using Domain.Spaces.ValueObjects;
using Domain.ValueObjects;
using MediatR;

namespace Application.Spaces.Commands;

public static class AddSpace
{
    public sealed class Command : IRequest
    {
        public Command(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [RequiredGuid(ErrorMessage = EntityId.EmptyMessage)]
        public Guid Id { get; }

        [Required(ErrorMessage = SpaceName.EmptyMessage)]
        [MaxLength(SpaceName.MaximumLength, ErrorMessage = SpaceName.MaximumLengthMessage)]
        public string Name { get; }
    }

    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly ISpaceRepository _spaceRepository;

        public Handler(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var space = new Space(new(request.Id), new(request.Name));
            await _spaceRepository.Save(space);

            return Unit.Value;
        }
    }
}