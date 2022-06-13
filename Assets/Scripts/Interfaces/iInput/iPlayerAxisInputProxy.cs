using System;


public interface iPlayerAxisInputProxy
{
    event Action<float> OnMouseAxisChange;
    void GetAxis();
}
