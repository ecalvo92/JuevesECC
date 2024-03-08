USE [master]
GO

CREATE DATABASE [jueves_db]
GO

USE [jueves_db]
GO

CREATE TABLE [dbo].[tCategoria](
	[IdCategoria] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[CantidadMinima] [int] NOT NULL,
 CONSTRAINT [PK_tCategoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tProducto](
	[IdProducto] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Inventario] [int] NOT NULL,
	[IdCategoria] [bigint] NOT NULL,
 CONSTRAINT [PK_tProducto] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tRol](
	[IdRol] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Correo] [varchar](200) NOT NULL,
	[Contrasenna] [varchar](200) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[IdRol] [smallint] NOT NULL,
	[Estado] [bit] NOT NULL,
	[EsTemporal] [bit] NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tCategoria] ON 
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre], [CantidadMinima]) VALUES (1, N'Arroz con pollo', 20)
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre], [CantidadMinima]) VALUES (3, N'Sopa de mariscos', 10)
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre], [CantidadMinima]) VALUES (4, N'Spaguetti', 15)
GO
SET IDENTITY_INSERT [dbo].[tCategoria] OFF
GO

SET IDENTITY_INSERT [dbo].[tProducto] ON 
GO
INSERT [dbo].[tProducto] ([IdProducto], [Nombre], [Inventario], [IdCategoria]) VALUES (1, N'Pasta', 50, 4)
GO
SET IDENTITY_INSERT [dbo].[tProducto] OFF
GO

SET IDENTITY_INSERT [dbo].[tRol] ON 
GO
INSERT [dbo].[tRol] ([IdRol], [Nombre]) VALUES (1, N'Cliente')
GO
INSERT [dbo].[tRol] ([IdRol], [Nombre]) VALUES (2, N'Trabajador Administrativo')
GO
INSERT [dbo].[tRol] ([IdRol], [Nombre]) VALUES (3, N'Trabajador Operativo')
GO
SET IDENTITY_INSERT [dbo].[tRol] OFF
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([IdUsuario], [Correo], [Contrasenna], [Nombre], [IdRol], [Estado], [EsTemporal]) VALUES (5, N'msanchez00881@ufide.ac.cr', N'T0xLUmcdLgwAnRluCxHc6Q==', N'Mario Sánchez Monge', 1, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tProducto]  WITH CHECK ADD  CONSTRAINT [FK_tProducto_tProducto] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[tCategoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[tProducto] CHECK CONSTRAINT [FK_tProducto_tProducto]
GO

ALTER TABLE [dbo].[tUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tUsuario_tRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[tRol] ([IdRol])
GO
ALTER TABLE [dbo].[tUsuario] CHECK CONSTRAINT [FK_tUsuario_tRol]
GO

CREATE PROCEDURE [dbo].[CambiarContrasenna]
	@Correo					VARCHAR(200),
	@Contrasenna			VARCHAR(200),
	@ContrasennaTemporal	VARCHAR(200),
	@EsTemporal				BIT
AS
BEGIN

	DECLARE @Consecutivo BIGINT

	SELECT @Consecutivo = IdUsuario
	FROM	tUsuario
	WHERE	Correo = @Correo
		AND Contrasenna = @ContrasennaTemporal
		AND Estado = 1

	IF(@Consecutivo IS NOT NULL)
	BEGIN
		UPDATE	tUsuario
		SET		Contrasenna = @Contrasenna,
				EsTemporal  = @EsTemporal
		WHERE	Correo = @Correo
	END

	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado
	FROM	tUsuario U
	INNER	JOIN tRol R ON U.IdRol = R.IdRol	
	WHERE	Correo = @Correo

END
GO

CREATE PROCEDURE [dbo].[ConsultarProductos]
AS
BEGIN
	
	SELECT IdProducto,P.Nombre 'NombreProducto',Inventario,P.IdCategoria,C.Nombre 'NombreCategoria',CantidadMinima
	FROM	tProducto P
	INNER JOIN tCategoria C ON P.IdCategoria = C.IdCategoria

END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(200),
	@Contrasenna	varchar(200)
AS
BEGIN
	
	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado,EsTemporal
	FROM	tUsuario U
	INNER	JOIN tRol R ON U.IdRol = R.IdRol
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[RecuperarAcceso]
	@Correo			VARCHAR(200),
	@Contrasenna	VARCHAR(200),
	@EsTemporal		BIT
AS
BEGIN

	DECLARE @Consecutivo BIGINT

	SELECT @Consecutivo = IdUsuario
	FROM	tUsuario
	WHERE	Correo = @Correo
		AND Estado = 1

	IF(@Consecutivo IS NOT NULL)
	BEGIN
		UPDATE	tUsuario
		SET		Contrasenna = @Contrasenna,
				EsTemporal  = @EsTemporal
		WHERE	Correo = @Correo
	END

	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado
	FROM	tUsuario U
	INNER	JOIN tRol R ON U.IdRol = R.IdRol	
	WHERE	Correo = @Correo

END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Correo			varchar(200),
	@Contrasenna	varchar(200),
	@NombreUsuario	varchar(200)
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM tUsuario WHERE Correo = @Correo)
	BEGIN

		INSERT INTO dbo.tUsuario(Correo,Contrasenna,Nombre,IdRol,Estado,EsTemporal)
	    VALUES(@Correo,@Contrasenna,@NombreUsuario,1,1,0)

	END

END
GO