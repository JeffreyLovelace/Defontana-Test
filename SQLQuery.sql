

SELECT SUM(Total) AS TotalVentas, COUNT(*) AS CantidadVentas
FROM Venta
WHERE Fecha >= DATEADD(day, -30, GETDATE()) AND Fecha <= GETDATE()

SELECT TOP 1 Fecha, Total
FROM Venta
WHERE Fecha >= DATEADD(day, -30, GETDATE()) AND Fecha <= GETDATE()
ORDER BY Total DESC

SELECT TOP 1 p.Nombre AS Producto, SUM(vd.TotalLinea) AS MontoTotalVentas
FROM VentaDetalle vd
JOIN Producto p ON vd.ID_Producto = p.ID_Producto
JOIN Venta v ON vd.ID_Venta = v.ID_Venta
WHERE v.Fecha >= DATEADD(day, -30, GETDATE()) AND v.Fecha <= GETDATE()
GROUP BY p.Nombre
ORDER BY MontoTotalVentas DESC

SELECT TOP 1 l.Nombre AS Local, SUM(v.Total) AS MontoTotalVentas
FROM Venta v
JOIN Local l ON v.ID_Local = l.ID_Local
WHERE v.Fecha >= DATEADD(day, -30, GETDATE()) AND v.Fecha <= GETDATE()
GROUP BY l.Nombre
ORDER BY MontoTotalVentas DESC


SELECT TOP 1 m.Nombre AS Marca, SUM(vd.TotalLinea) AS MargenGanancias
FROM VentaDetalle vd
JOIN Producto p ON vd.ID_Producto = p.ID_Producto
JOIN Marca m ON p.ID_Marca = m.ID_Marca
JOIN Venta v ON vd.ID_Venta = v.ID_Venta
WHERE v.Fecha >= DATEADD(day, -30, GETDATE()) AND v.Fecha <= GETDATE()
GROUP BY m.Nombre
ORDER BY MargenGanancias DESC


SELECT l.Nombre AS Local, p.Nombre AS Producto
FROM (
SELECT v.ID_Local, vd.ID_Producto, ROW_NUMBER() OVER (PARTITION BY v.ID_Local ORDER BY COUNT(*) DESC) AS RowNumber
FROM Venta v
JOIN VentaDetalle vd ON v.ID_Venta = vd.ID_Venta
WHERE v.Fecha >= DATEADD(day, -30, GETDATE()) AND v.Fecha <= GETDATE()
GROUP BY v.ID_Local, vd.ID_Producto
) AS SalesRanking
JOIN Local l ON SalesRanking.ID_Local = l.ID_Local
JOIN Producto p ON SalesRanking.ID_Producto = p.ID_Producto
WHERE RowNumber = 1