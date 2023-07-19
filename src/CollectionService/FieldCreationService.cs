
using Common.EFCore;
using Common.Models;
using Common.Repositories;

namespace CollectionService
{
    public  class FieldCreationService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AppDbContext _context;
        private readonly IFieldRepository<Field> _repository;

        public FieldCreationService(AppDbContext context, IFieldRepository<Field> repository)
         {
             _context = context;
             _repository = repository;
         }
        public async Task CreateField(Dictionary<int,string> fieldsType, Guid CollectionId)
        {
        
          
            var Fields = new Fields() {CollectionId = CollectionId};
             _context.Fields.Add(Fields);
             await _context.SaveChangesAsync();
            var FieldsId = Fields.Id;
            foreach (var item in fieldsType)
            {
                System.Console.WriteLine("In foreach!");
                var message = item.Key switch
                {
                    1 => CreateTypedFieldAsync<FieldInt>(new FieldInt{Name = item.Value, FieldsId = FieldsId}),
                    2 => CreateTypedFieldAsync<FieldString>(new FieldString{Name = item.Value, FieldsId = FieldsId}),
                    3 => CreateTypedFieldAsync<FieldText>(new FieldText{Name = item.Value, FieldsId = FieldsId}),
                    4 => CreateTypedFieldAsync<FieldBool>(new FieldBool{Name = item.Value, FieldsId = FieldsId}),
                    5 => CreateTypedFieldAsync<FieldDate>(new FieldDate{Name = item.Value, FieldsId = FieldsId}),
                    _ => throw new ArgumentOutOfRangeException("Value must to be between 1 and 6 included")
        
                };
                
                message.GetAwaiter().GetResult();
                System.Console.WriteLine(message.IsCompletedSuccessfully);
            }
           
        
        
        }

        public async Task CreateTypedFieldAsync<T>(Field field)
        {
            if (field == null)
                throw new ArgumentNullException($"{nameof(field)} cann't be a null");

            await _context.AddAsync(field);
            await _context.SaveChangesAsync();
        }
            
           
    }
}