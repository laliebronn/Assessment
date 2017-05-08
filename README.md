# Assessment
Always Get Something Out

Technical Approach to Assessment Solution: Lalie Brönn

There really are a million and one ways to get the desired result, and choosing the right one is harder than one
anticipates at first, especially since this is not within real world context of business needs. For instance, if this was
applied to a massive set of data, or the entire active book, one would never work in CSV format, but export to an
appropriate, normalise database and work with stored procedures. If this was a regular requirement, one would
write a service that will automatically do this once a day, or as often as required through scheduled services.

As none of this information is available, one would have to assume that the quickest and easiest solution would
meet the business need in the least amount of time. Therefore, I would tackle the solution as follows:

- Firstly, I would programmatically split the street number and street name into two separate fields. This way
the major processing will happen once at the beginning of the process, and not individually on field level
when I have to sort by street name later on.
- I would use the CSV file ODBC functionality to then read the CSV file into a data table in C#.
- From there I would use either LINQ or a nested SQL statement to obtain the UNIQUE string from name and
surname, and then count their occurrence across both fields throughout the data set.
- This would give me the First list required which I would publish to a new CSV file.
- Given that the street name now has its own field in the datatable already created, selecting the
concatenated number and name, sorted alphabetically by the street name is easy as pie. Same procedure
would then be followed to publish this result to its own CSV file as well.

As for the unit testing, I would include the following test cases to confirm quality:
- Test less usual names and surnames, like O’Bryan and surnames with spaces, like Van der Merwe.
- Test cases where the name and surname are the same on the same line, like Scott Scott.
- Test cases where you have mixtures of partly matching items, like Johnson and John or Matt and Matthews
both in surnames and names
- Test with empty names or surnames, addresses or street numbers or other fields empty
- Test error handling when the file is missing or the CSV has an incorrect format, etc.
- Test addresses where the first character of the street name is a number, like 7 th street
- Test addresses where there is no number, for instance Cnr Rose and Daisy ave
- Test addresses with letter/spaces in the street number, for instance 23 A Sickle road.
- Obviously all testing should include your standard security testing like Sub Select SQL, etc.
