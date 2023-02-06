
SELECT* FROM Products
--!Write a query to get a Product list (id, name, unit price) where current products cost less than $20.
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE ((UnitPrice)<20) 
ORDER BY UnitPrice ;

--Write a query to get Product list (id, name, unit price) where products cost between $15 and $25
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE (((UnitPrice)>15) AND ((UnitPrice)<25))
ORDER BY UnitPrice ;

--Write a query to get Product list (name, unit price) of above average price.
SELECT ProductName, UnitPrice
FROM Products
WHERE UnitPrice > (SELECT avg(UnitPrice) FROM Products)
ORDER BY UnitPrice;

--Write a query to get Product list (name, unit price) of ten most expensive products
 (SELECT MAX(UnitPrice) FROM Products)

 -- Write a query to count current and discontinued products
 SELECT Count(ProductName)
FROM Products
GROUP BY Discontinued;

--Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order
SELECT ProductName, unitsonorder ,unitsinstock
FROM Products
WHERE ((unitsonorder)>(unitsinstock))
ORDER BY UnitPrice;
