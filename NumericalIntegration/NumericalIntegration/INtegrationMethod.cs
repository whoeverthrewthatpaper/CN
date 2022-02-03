namespace NumericalIntegration
{
    internal interface INtegrationMethod
    {
        double TrapezodalMethod(int numberOfInterval);

        double SimpsonMethod(int numberOfInterval);
    }

}
