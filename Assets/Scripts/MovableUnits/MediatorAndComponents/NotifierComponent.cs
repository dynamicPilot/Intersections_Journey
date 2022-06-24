namespace IJ.MovableUnits.MediatorAndComponents
{
    public class NotifierComponent
    {
        protected IMediator mediator;

        public NotifierComponent(IMediator _mediator = null)
        {
            mediator = _mediator;
        }

        public void SetMediator(IMediator _mediator)
        {
            mediator = _mediator;
        }
    }
}