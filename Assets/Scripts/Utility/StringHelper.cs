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
         Log.Error($"�÷��ڵ忡 #�� �پ���մϴ�!");
#endif
            return Color.white;
        }

        var color = ColorUtility.TryParseHtmlString(_colorCode, out Color value);
        if (color == false)
        {
#if LOG
         Log.Error($"�ش� �÷��� ��ȯ�� �� �����ϴ�!");
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
    /// <summary>����(����)�� Ư�� �ڸ������� 0�� ä�� ���ڿ��� ��ȯ</summary>
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

    /// <summary>�Ҽ��� ���� Ư�� �ڸ��������� ǥ���� �� ���ڿ��� ��ȯ</summary>
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

    /// <summary>����Ʈ(����)�� Gb, Mb, Kb, Bytes�� �����Ͽ� ���ڿ��� ��ȯ</summary>
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
    /// StringSetter ����
    /////////////

    /// <summary>��¥�Է½� �ش糯¥ ��12�ð�������</summary>
    public static DateTime ToDateTime(this string _str)
    {
        //DateTime dti01 = Convert.ToDateTime("2015 11 13");      //��ȯ ���� (2015�� 11�� 13�� ���� 12:00:00)
        //DateTime dti02 = Convert.ToDateTime("2015-11-13");      //��ȯ ���� (2015�� 11�� 13�� ���� 12:00:00)
        //DateTime dti03 = Convert.ToDateTime("2015/11/13");      //��ȯ ���� (2015�� 11�� 13�� ���� 12:00:00)
        //DateTime dti04 = Convert.ToDateTime("2015-11/13");      //��ȯ ���� (2015�� 11�� 13�� ���� 12:00:00)
        //DateTime dti05 = Convert.ToDateTime("2015/11 13");
        var time = DateTime.TryParse(_str, out DateTime _value);
        if (time == false)
        {
#if LOG
          Log.Message($"DateTime ���� ��ȯ �����Դϴ�! �ش� ������ �ش����� ������������ �� �ֽ��ϴ� or string : {_str}, �������� : 2015 11 11, 2015-11-13, 2015/11/13");
#endif
            return DateTime.MinValue;
        }

        return _value;
    }

    /// <summary>���� ǥ�� float </summary>
    public static string ToIntString(this float value)
    {
        return value.ToString("N0");
    }

    /// <summary>���� ǥ�� int </summary>
    public static string ToIntString(this int value)
    {
        return value.ToString("N0");
    }

    /// <summary>���� ǥ�� long </summary>
    public static string ToIntString(this long value)
    {
        return value.ToString("N0");
    }

    /// <summary>���� ǥ�� Column ex)1,000 </summary>
    public static string ToColumnString(this int value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>���� ǥ�� Column ex)1,000 </summary>
    public static string ToColumnString(this double value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>���� ǥ�� Column ex)1,000 </summary>
    public static string ToColumnString(this long value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary>���� ǥ�� Column ex)1,000 </summary>
    public static string ToColumnString(this float value)
    {
        var val = string.Format("{0:N0}", value);
        return val;
    }

    /// <summary> �Ҽ��� 1�ڸ��� ǥ��(%),�Ҽ��� ������ round </summary>
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
    /// <summary> �Ҽ��� 2�ڸ��� ǥ��(%),�Ҽ��� ������ round </summary>
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
    /// <summary> �Ҽ��� 1�ڸ��� ǥ��(%),�Ҽ��� ������ round </summary>
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
    /// <summary>�Ҽ��� 2�ڸ����� ǥ�� ex) 1.00=> 1 , 1.01=>1.01, 1.013=> 1.01 </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFloatN2String(this float value)
    {
        //2�ڸ�ǥ��
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

    //���� �ѱۺ���ǥ��
    public static string ConvertValuePriceString(this long price)
    {
        string num = string.Format("{0:# #### #### #### #### ####}", price).TrimStart().Replace(" ", ",");

        string[] unit = new string[] { "", "��", "��", "��", "��", "��" };
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
