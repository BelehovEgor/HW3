using Models;

namespace ServerAdapter.Commands
{
    public interface IPutCommand
    {
        void Execute(User user);
    }
}
