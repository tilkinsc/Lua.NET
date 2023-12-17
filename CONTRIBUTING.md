## How to Contribute

1. Follow the code style
```cs
if (...)
{
	
}

if (...)
	return;

try
{
	
}
catch (...)
{
	
}

for (int i = 0; i<=1; i++)
{
	
}

foreach (Type variable = new Type())
{
	
}

foreach (var variable = new Dictionary<string, string>().keys)
{
	
}

while (...)
{
	
}

using (...)
{
	
}

private int _name;
public int Name;
public int Name { get; private set; }
Only use var when using foreach with a dictionary's key or value sets.

public void Example(int argName)
{
	
}

~bit
~(bit ^ 0xFF)
((bit & 0xFF) | 0xFF) >> 0xFF
```

2. Push changes to a new local branch to the 'dev' branch. ('contributor-feature' -> 'dev')
3. You are encouraged to start a PR early so we can spy on what you are working on! [How to do that](https://github.blog/2019-02-14-introducing-draft-pull-requests/)
4. Versions should not be bumped at all
5. gitignore is free game add anything that fits
6. Do not use #region
7. Tabs only. Spaces will not be accepted.
