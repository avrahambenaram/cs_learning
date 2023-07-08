namespace cs_learn.config;

public class ValueObjectsConfig
{
    public static Lengther name = new Lengther(2, 30);
    public static Lengther email = new Lengther(2, 60);
    public static Lengther password = new Lengther(5, Int32.MaxValue);
}