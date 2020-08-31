# makemagic
<b>ASP.NET Core 3.1 CRUD application</b> for Harry Potter characters.
<p>It is integrated with this <a href="https://www.potterapi.com" rel="nofollow">API</a> to check if the house really exists.</p>
<p>The project is using Entity Framework InMemmory Database. For production environment configure some better Database.

This is an example of json acceptable:
<pre><code>{
    "name": "Harry Potter",
    "role": "student",
    "school": "Hogwarts School of Witchcraft and Wizardry",
    "house": "5a05e2b252f721a3cf2ea33f",
    "patronus": "stag"
}
</code></pre>

<h2>The application is able to:</h2>

<ul>
    <li><b>Create a new character:</b> POST api/v1.0/characters</li>
<li><b>Read a single character:</b> GET api/v1.0/characters/{id}</li>
<li><b>Read all characters:</b> GET api/v1.0/characters</li>
<li><b>Update a single character:</b> PUT api/v1.0/characters/{id}</li>
<li><b>Delete a single character:</b> DELETE api/v1.0/characters/{id}</li>
</ul>

<h2>Configurations</h2>

<ul>
<li><b>PotterAPI key:</b> Put your api key into the file <code>Settings.cs -> Secret = "PASTE YOUR POTTERAPI KEY HERE";</code></li>
</ul>
