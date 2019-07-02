enum KarmaValue
{
  Plus,
  None,
  Minus
}

KarmaValue getKarmaValueFromInt(int value)
{
  if (value == 0)
  {
    return KarmaValue.None;
  }
  else if (value > 0)
  {
    return KarmaValue.Plus;
  }
  else if (value < 0)
  {
    return KarmaValue.Minus;
  }

  return KarmaValue.None;
}