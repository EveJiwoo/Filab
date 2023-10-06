using System;
using TMPro;
using UnityEngine;

static public class StringHelper
{
    public static Color ToColorHtml(this TextMeshProUGUI _txt, string _colorCode)
    {
        if (_colorCode.Contains("#") == false)
        {
#if LOG
         Log.Error($"컬러코드에 #이 붙어야합니다!");
#endif
            return Color.white;
        }

        var color = ColorUtility.TryParseHtmlString(_colorCode, out Color value);
        if (color == false)
        {
#if LOG
         Log.Error($"해당 컬러로 변환할 수 없습니다!");
#endif
            return Color.white;
        }
        _txt.color = value;
        return value;
    }
    public static bool IsJson(string _json)
    {
        if (string.IsNullOrEmpty(_json))
            return false;
        var split = _json.ToCharArray();
        if (split[0] == '[')
            return true;
        if (split[0] == '{')
            return true;

        return false;
    }
    /// <summary>숫자(정수)를 특정 자릿수까지 0을 채워 문자열로 반환</summary>
    static public string ConvertInteger_D(int _num, int _fillCnt)
    {
        switch (_fillCnt)
        {
            case 0:
            case 1:
                break;
            case 2: return string.Format("{0:D2}", _num);
            case 3: return string.Format("{0:D3}", _num);
            case 4: return string.Format("{0:D4}", _num);
            case 5: return string.Format("{0:D5}", _num);
        }

        return "Please try, Max fillCnt = 5";
    }

    /// <summary>소수점 이하 특정 자릿수까지만 표현한 뒤 문자열로 반환</summary>
    static public string ConvertDecimal_N(float _num, int _fillCnt)
    {
        switch (_fillCnt)
        {
            case 0:
            case 1:
                break;
            case 2: return string.Format("{0:N2}", _num);
            case 3: return string.Format("{0:N3}", _num);
            case 4: return string.Format("{0:N4}", _num);
            case 5: return string.Format("{0:N5}", _num);
        }

        return "Please try, Max fillCnt = 5";
    }

    /// <summary>바이트(정수)를 Gb, Mb, Kb, Bytes로 구분하여 문자열로 반환</summary>
    static public string FormatBytes(long bytes)
    {
        const int scale = 1024;
        string[] orders = new string[] { "Gb", "Mb", "Kb", "Bytes" };
        long max = (long)Mathf.Pow(scale, orders.Length - 1);

        foreach (string order in orders)
        {
            if (bytes > max)
                return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

            max /= scale;
        }
        return "0 Bytes";
    }

    //////////////
    /// StringSetter 병합
    /////////////

    /// <summary>날짜입력시 해당날짜 밤12시가져오기</summary>
    public static DateTime ToDateTime(this string _str)
    {
        //DateTime dti01 = Convert.ToDateTime("2015 11 13");      //변환 가능 (2015년 11월 13일 오후 12:00:00)
        //DateTime dti02 = Convert.ToDateTime("2015-11-13");      //변환 가능 (2015년 11월 13일 오후 12:00:00)
        //DateTime dti03 = Convert.ToDateTime("2015/11/13");      //변환 가능 (2015년 11월 13일 오후 12:00:00)
        //DateTime dti04 = Convert.ToDateTime("2015-11/13");      //변환 가능 (2015년 11월 13일 오후 12:00:00)
        //DateTime dti05 = Convert.ToDateTime("2015/11 13");
        var time = DateTime.TryParse(_str, out DateTime _value);
        if (time == false)
        {
#if LOG
          Log.Message($"DateTime 형식 변환 오류입니다! 해당 월에는 해당일이 존재하지않을 수 있습니다 or string : {_str}, 가능형식 : 2015 11 11, 2015-11-13, 2015/11/13");
#endif
            return DateTime.MinValue;
        }

        return _value;
    }

    /// <summary>정수 표기 float </summary>
    public static string ToIntString(this float value)
    {
        return value.ToString("N0");
    }

    /// <summary>정수 표기 int </summary>
    public static string ToIntString(this int value)
    {
        return value.ToString("N0");
    }

    /// <summary>정수 표기 long </summary>
    public static string ToIntString(this long value)
    {
        return value.ToString("N0");
    }

    /// <summary>정수 표기 Column ex)1,000 </summary>
    public static string ToColumnString(this int value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>정수 표기 Column ex)1,000 </summary>
    public static string ToColumnString(this double value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>정수 표기 Column ex)1,000 </summary>
    public static string ToColumnString(this long value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>정수 표기 Column ex)1,000 </summary>
    public static string ToColumnString(this float value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary> 소수점 1자리수 표시(%),소수가 나오면 round </summary>
    public static string ToPerString(this float value)
    {
        value *= 100;
        var valueStr = $"{value:N1}";


        if (valueStr.Contains(".0") || value == 0)
        {
            var result = Mathf.RoundToInt(value);
            return $"{result:N0}%";
        }
        else
        {
            return $"{value:N1}%";
        }
    }
    /// <summary> 소수점 2자리수 표시(%),소수가 나오면 round </summary>
    public static string ToPerStringTwo(this float value)
    {
        value *= 100;
        var valueStr = $"{value:N1}";


        if (valueStr.Contains(".0") || value == 0)
        {
            var result = Mathf.RoundToInt(value);
            return $"{result:N0}%";
        }
        else
        {
            return $"{value:N2}%";
        }
    }
    /// <summary> 소수점 1자리수 표시(%),소수가 나오면 round </summary>
    public static string ToPerString(this int value)
    {
        value *= 100;
        var valueStr = $"{value:N1}";


        if (valueStr.Contains(".0") || value == 0)
        {
            var result = Mathf.RoundToInt(value);
            return $"{result:N0}%";
        }
        else
        {
            return $"{value:N1}%";
        }

    }

    public static string ToPerThirdPrimeString(this float value)
    {
        value *= 100;
        var valueStr = $"{value:N3}";


        if (valueStr.Contains(".000") || value == 0)
        {
            var result = Mathf.RoundToInt(value);
            return $"{result:N0}%";
        }
        else
        {
            return $"{value:N3}%";
        }
    }
    public static string ToFloatDisplayString(this float value)
    {
        value *= 1000;
        var valueStr = $"{value:N1}";


        if (valueStr.Contains(".0") || value == 0)
        {
            var result = Mathf.RoundToInt(value);
            return $"{result:N0}";
        }
        else
        {
            return $"{value:N1}";
        }

    }
    /// <summary>소수점 2자리까지 표기 ex) 1.00=> 1 , 1.01=>1.01, 1.013=> 1.01 </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFloatN2String(this float value)
    {
        //2자리표기
        var valueStr = value.ToString("N2");
        if (valueStr.Contains(".00"))
        {
            valueStr = value.ToString("N0");
            return valueStr;
        }

        return valueStr;
    }

    public static string ConvertValuePriceString(this int _value)
    {
        return ConvertValuePriceString((long)_value);
    }

    //숫자 한글병합표기
    public static string ConvertValuePriceString(this long price)
    {
        string num = string.Format("{0:# #### #### #### #### ####}", price).TrimStart().Replace(" ", ",");

        string[] unit = new string[] { "", "만", "억", "조", "경", "해" };
        string[] str = num.Split(',');
        string result = "";
        int cnt = 0;
        for (int i = str.Length; i > 0; i--)
        {
            if (Convert.ToInt32(str[i - 1]) != 0)
            {
                result = Convert.ToInt32(str[i - 1]) + unit[cnt] + result;
            }
            cnt++;
        }
        return result;
    }
}
