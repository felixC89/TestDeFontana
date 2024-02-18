--query 1
--El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
SELECT
	SUM(vd.TotalLinea) as Suma_TotalLinea,
	SUM(vd.Cantidad) as Suma_Cantidad
FROM
	VentaDetalle vd
Inner JOIN Venta v ON
	vd.ID_Venta = v.ID_Venta
Inner JOIN Local l ON
	v.ID_Local = l.ID_Local
Inner JOIN Producto p ON
	vd.ID_Producto = p.ID_Producto
Inner JOIN Marca m ON
	p.ID_Marca = m.ID_Marca
WHERE
	v.Fecha >= '2024-03-16 00:00:00.000'
	and v.Fecha <= '2024-04-17 00:00:00.000';

--query 2
-- El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto)
SELECT
	TOP 1
	v.Fecha,
	vd.TotalLinea
FROM
	VentaDetalle vd
Inner JOIN Venta v ON
	vd.ID_Venta = v.ID_Venta
Inner JOIN Local l ON
	v.ID_Local = l.ID_Local
Inner JOIN Producto p ON
	vd.ID_Producto = p.ID_Producto
Inner JOIN Marca m ON
	p.ID_Marca = m.ID_Marca
WHERE
	v.Fecha >= '2024-03-16 00:00:00.000'
	and v.Fecha <= '2024-04-17 00:00:00.000'
ORDER BY
	vd.TotalLinea desc;


--query 3
--Indicar cuál es el producto con mayor monto total de ventas.
SELECT
	top 1
	p.Nombre,
	SUM(vd.TotalLinea) as Suma_TotalLinea
FROM
	VentaDetalle vd
Inner JOIN Venta v ON
	vd.ID_Venta = v.ID_Venta
Inner JOIN Local l ON
	v.ID_Local = l.ID_Local
Inner JOIN Producto p ON
	vd.ID_Producto = p.ID_Producto
Inner JOIN Marca m ON
	p.ID_Marca = m.ID_Marca
WHERE
	v.Fecha >= '2024-03-16 00:00:00.000'
	and v.Fecha <= '2024-04-17 00:00:00.000'
GROUP BY
	p.Nombre
ORDER BY
	Suma_TotalLinea DESC;

--query 4
--Indicar el local con mayor monto de ventas.

SELECT
	top 1
    l.Nombre,
	SUM(vd.TotalLinea) as Suma_TotalLinea
FROM
	VentaDetalle vd
Inner JOIN Venta v ON
	vd.ID_Venta = v.ID_Venta
Inner JOIN Local l ON
	v.ID_Local = l.ID_Local
Inner JOIN Producto p ON
	vd.ID_Producto = p.ID_Producto
Inner JOIN Marca m ON
	p.ID_Marca = m.ID_Marca
WHERE
	v.Fecha >= '2024-03-16 00:00:00.000'
	and v.Fecha <= '2024-04-17 00:00:00.000'
GROUP BY
	l.Nombre
ORDER BY
	Suma_TotalLinea DESC;

--query 5
--¿Cuál es la marca con mayor margen de ganancias?
SELECT
	top 1
    m.Nombre,
	SUM(vd.TotalLinea) as Suma_TotalLinea
FROM
	VentaDetalle vd
Inner JOIN Venta v ON
	vd.ID_Venta = v.ID_Venta
Inner JOIN Local l ON
	v.ID_Local = l.ID_Local
Inner JOIN Producto p ON
	vd.ID_Producto = p.ID_Producto
Inner JOIN Marca m ON
	p.ID_Marca = m.ID_Marca
WHERE
	v.Fecha >= '2024-03-16 00:00:00.000'
	and v.Fecha <= '2024-04-17 00:00:00.000'
GROUP BY
	m.Nombre
ORDER BY
	Suma_TotalLinea DESC;


--query 6
--Cómo obtendrías cuál es el producto que más se vende en cada local
select
	datos.NombreLocal,
	(
	select
		Nombre
	from
		Producto
	where
		ID_Producto = datos.ID_Producto) as NombreProducto,
	datos.Suma_TotalLinea
from
	(
	SELECT
		l.Nombre as NombreLocal,
		p.ID_Producto,
		SUM(vd.TotalLinea) as Suma_TotalLinea,
		ROW_NUMBER() OVER(PARTITION BY l.Nombre
	ORDER BY
		SUM(vd.TotalLinea) DESC) as rn
	FROM
		VentaDetalle vd
	Inner JOIN Venta v ON
		vd.ID_Venta = v.ID_Venta
	Inner JOIN Local l ON
		v.ID_Local = l.ID_Local
	Inner JOIN Producto p ON
		vd.ID_Producto = p.ID_Producto
	Inner JOIN Marca m ON
		p.ID_Marca = m.ID_Marca
	WHERE
		v.Fecha >= '2024-03-16 00:00:00.000'
		and v.Fecha <= '2024-04-17 00:00:00.000'
	GROUP BY
		l.Nombre,
		p.ID_Producto) as datos
where
	datos.rn = 1;