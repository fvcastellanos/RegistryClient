namespace RegistryClient.Domain
{
    public class Result<TL, TR>
    {
        private readonly TL _left;
        private readonly TR _right;
        
        private Result(TL left)
        {
            _left = left;
            _right = default(TR);
        }

        private Result(TR right)
        {
            _left = default(TL);
            _right = right;
        }

        public static Result<TL, TR> ForLeft(TL left)
        {
            return new Result<TL, TR>(left);
        }

        public static Result<TL, TR> ForRight(TR right)
        {
            return new Result<TL, TR>(right);
        }

        public bool IsLeft()
        {
            return _left != null ? true : false;
        }

        public TR Right => _right;

        public TL Left => _left;
    }
}