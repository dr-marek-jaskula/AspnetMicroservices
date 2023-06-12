## MongoDb Commands

We use mongo shell so **mongosh**. This is based on a javaScript, so the case will be camelCase

Show databases
> show dbs

Default ones are "admin", "config" and "local"

Create a new db named "CatalogDb"
> use CatalogDb

Show collections
> show collections

Create a new collection inside the database (it is like a table in sql)
> db.createCollection('Products')

Insert items to the collection 'Products'
```shell
db.Products.insertOne(
	{
		"Name": "Asus Laptop",
		"Category": "Computers",
		"Summary": "Summary",
		"Description": "Description",
		"ImageFile": "ImageFile",
		"Price": 54.93
	}
)
```

Or insert many at once 
```shell
db.Products.insertMany(
[
	{
		"Name": "Asus Laptop",
		"Category": "Computers",
		"Summary": "Summary",
		"Description": "Description",
		"ImageFile": "ImageFile",
		"Price": 54.93
	},
	{
		"Name": "HP Laptop",
		"Category": "Computers",
		"Summary": "Summary",
		"Description": "Description",
		"ImageFile": "ImageFile",
		"Price": 88.93
	}
])	
```

Then we get: 
```
{
  acknowledged: true,
  insertedIds: {
    '0': ObjectId("6486eb47d8b61966776b2be2"),
    '1': ObjectId("6486eb47d8b61966776b2be3")
  }
}
```

To remove all:

> db.Products.remove({})

## Querying in mongo

To query all we do not specify filters (like SELECT * FROM Products)
> db.Products.find()

To specify filters (SELECT * FROM Products WHERE Name = "Asus Laptop")
> db.Products.find( { "Name": "HP Laptop" } )

Other Query (SELECT * FROM movies WHERE rated in ("PG", "PG-13"))
> db.Products.find( { Name: { $in: [ "HP Laptop", "Asus Laptop" ] } } )

AND Logical Operators (gte is "greather then), comma means "and"
> db.Products.find( { Name: "HP Laptop", Price: { $gte: 60 } } )

OR Logical Operator (this state for Category = "Computers" AND (Price < 70 OR Name = "HP Laptop")

```
db.Products.find( {
     Category: "Computers",
     $or: [ { "Price": { $lt: 70 } }, { Name: "HP Laptop" } ]
} )
```


