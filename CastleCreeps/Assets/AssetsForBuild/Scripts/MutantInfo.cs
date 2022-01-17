using UnityEngine;

public class MutantInfo
{
    public GameObject mutantObj;
    public HPAttributes HPA;

    public MutantInfo(GameObject mutantObj, HPAttributes hPAttribute)
    {
        this.mutantObj = mutantObj;
        HPA = hPAttribute;
    }

}

public class HPAttributes
{
    public string hPString;
    public int hPValue;

    public HPAttributes(string hPString, int hPValue)
    {
        this.hPString = hPString;
        this.hPValue = hPValue;
    }
}
