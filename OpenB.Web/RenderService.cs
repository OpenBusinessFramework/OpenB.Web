namespace OpenB.Web
{
    public class RenderService
    {
        private WebSolution webSolution;

        public RenderService(WebSolution webSolution)
        {
            if (webSolution == null)
                throw new System.ArgumentNullException(nameof(webSolution));

            this.webSolution = webSolution;
        }       


    }
}
