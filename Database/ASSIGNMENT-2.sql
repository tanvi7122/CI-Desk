select *from orders
select * from customer
select* from salesman

--write a SQL query to find the salesperson and customer who reside in the same city. Return Salesman, cust_name and city
select c.cust_name,s.name,c.city from customer c join salesman s
on customer.city=salesman.city;