USE [HCMDGCBCC]
GO
/****** Object:  Table [dbo].[CauTraLoi]    Script Date: 10/21/2015 5:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauTraLoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenCauTraLoi] [nvarchar](1000) NULL,
	[GiaTri] [int] NULL,
	[TenVanTat] [nvarchar](100) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_CauTraLoi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InputTypeTieuChi]    Script Date: 10/21/2015 5:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InputTypeTieuChi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_InputType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhomTieuChi]    Script Date: 10/21/2015 5:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomTieuChi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNhomTieuChi] [nvarchar](500) NULL,
	[MaNhomTieuChi] [nvarchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_NhomChiTieu_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TieuChi]    Script Date: 10/21/2015 5:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TieuChi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenTieuChi] [nvarchar](1000) NULL,
	[NhomTieuChiID] [int] NULL,
	[TenVanTat] [nvarchar](200) NULL,
	[TypeInput] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_ChiTieu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TieuChiCauTraLoi]    Script Date: 10/21/2015 5:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TieuChiCauTraLoi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TieuChiID] [int] NULL,
	[CauTraLoiID] [int] NULL,
 CONSTRAINT [PK_TieuChiCauTraLoi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CauTraLoi] ON 

INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (1, N'Thuận tiện', 1, N'Thuận tiện', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (2, N'Tốt ', 1, N'Tốt ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (3, N'Niêm yết đầy đủ và có hướng dẫn rõ ràng, chi tiết', 1, N'Đầy đủ, rõ ràng, chi tiết', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (4, N'Một lần', 1, N'Một lần', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (5, N'Trước hẹn ', 1, N'Trước hẹn ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (6, N'Đã giải quyết xong', 1, N'Đã giải quyết xong', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (7, N'Chi phí thấp', 1, N'Thấp', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (8, N'Hài lòng', 1, N'Hài lòng', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (9, N'Bình thường', 0, N'Bình thường', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (10, N'Có niêm yết nhưng chưa có hướng dẫn chi tiết, còn phức tạp', 0, N'Có niêm yết nhưng còn phức tạp', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (11, N'Đúng hẹn', 0, N'Đúng hẹn', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (12, N'Không biết (vì chưa từng khiếu nại) ', 0, N'Không biết', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (13, N'Chi phí phù hợp', 0, N'Phù hợp', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (14, N'Khó khăn', -1, N'Khó khăn', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (15, N'Chưa tốt', -1, N'Chưa tốt', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (16, N'Chưa niêm yết đầy đủ ', -1, N'Chưa đầy đủ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (17, N'Không thuận tiện ', -1, N'Không thuận tiện ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (18, N'Hai lần trở lên', -1, N'Hai lần trở lên', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (19, N'Trễ hẹn ', -1, N'Trễ hẹn ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (20, N'Chưa giải quyết xong ', -1, N'Chưa giải quyết xong ', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (21, N'Không tốt', -1, N'Không tốt', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (22, N'Chi phí cao', -1, N'Cao', 1)
INSERT [dbo].[CauTraLoi] ([ID], [TenCauTraLoi], [GiaTri], [TenVanTat], [Active]) VALUES (23, N'Không hài lòng', -1, N'Không hài lòng', 1)
SET IDENTITY_INSERT [dbo].[CauTraLoi] OFF
SET IDENTITY_INSERT [dbo].[InputTypeTieuChi] ON 

INSERT [dbo].[InputTypeTieuChi] ([ID], [Name], [Value]) VALUES (1, N'Checkbox', 1)
INSERT [dbo].[InputTypeTieuChi] ([ID], [Name], [Value]) VALUES (2, N'TextArea', 2)
SET IDENTITY_INSERT [dbo].[InputTypeTieuChi] OFF
SET IDENTITY_INSERT [dbo].[NhomTieuChi] ON 

INSERT [dbo].[NhomTieuChi] ([ID], [TenNhomTieuChi], [MaNhomTieuChi], [Active]) VALUES (5466, N'TIẾP CẬN DỊCH VỤ', NULL, 1)
INSERT [dbo].[NhomTieuChi] ([ID], [TenNhomTieuChi], [MaNhomTieuChi], [Active]) VALUES (5467, N'THỦ TỤC HÀNH CHÍNH', NULL, 1)
INSERT [dbo].[NhomTieuChi] ([ID], [TenNhomTieuChi], [MaNhomTieuChi], [Active]) VALUES (5468, N'SỰ PHỤC VỤ CỦA CÔNG CHỨC', NULL, 1)
INSERT [dbo].[NhomTieuChi] ([ID], [TenNhomTieuChi], [MaNhomTieuChi], [Active]) VALUES (5469, N'KẾT QUẢ GIẢI QUYẾT CÔNG VIỆC', NULL, 1)
INSERT [dbo].[NhomTieuChi] ([ID], [TenNhomTieuChi], [MaNhomTieuChi], [Active]) VALUES (5470, N'ĐÁNH GIÁ TỶ LỆ HÀI LÒNG ĐỐI VỚI TOÀN BỘ QUÁ TRÌNH GIẢI QUYẾT THỦ TỤC HÀNH CHÍNH', NULL, 1)
SET IDENTITY_INSERT [dbo].[NhomTieuChi] OFF
SET IDENTITY_INSERT [dbo].[TieuChi] ON 

INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (30, N'Việc tìm hiểu thông tin về loại thủ tục hành chính mà ông/bà đang thực hiện như thế nào', NULL, N'Việc tìm hiểu thông tin về loại thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (31, N'Cơ sở vật chất tại nơi ông/bà đến thực hiện thủ tục hành chính như thế nào', NULL, N'Cơ sở vật chất  thực hiện thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (32, N'Việc niêm yết các quy định về trình tự, hồ sơ, biểu mẫu hành chính có liên quan đến loại thủ tục hành chính mà ông/bà đang thực hiện tại nơi ông/bà đến thực hiện thủ tục hành chính như thế nào', NULL, N'Việc niêm yết các quy định về trình tự, hồ sơ, biểu mẫu', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (33, N'Đánh giá của ông/bà về thái độ, tác phong của công chức tiếp nhận và trả kết quả giải quyết thủ tục hành chính cho ông/bà như thế nào', NULL, N'Về thái độ, tác phong của công chức tiếp nhận và trả kết quả giải quyết thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (34, N'Đánh giá của ông/bà về thái độ, tác phong, nghiệp vụ của công chức trực tiếp giải quyết thủ tục hành chính cho ông/bà như thế nào', NULL, N'Về thái độ, tác phong, nghiệp vụ của công chức trực tiếp giải quyết thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (35, N'Theo ông/bà thì quy trình giải quyết thủ tục hành chính của ông/bà có thuận tiện cho ông/bà chưa', NULL, N'Quy trình giải quyết thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (36, N'Số lần ông/bà phải liên hệ để hoàn tất thủ tục hành chính', NULL, N'Số lần phải liên hệ để hoàn tất thủ tục hành chính', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (37, N'Thời gian trả kết quả giải quyết thủ tục hành chính của ông/bà so với biên nhận', NULL, N'Thời gian trả kết quả giải quyết thủ tục hành chính ', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (38, N'Kết quả giải quyết hồ sơ của ông/bà tại thời điểm ông/bà trả lời khảo sát này', NULL, N'Kết quả giải quyết hồ sơ tại thời điểm trả lời khảo sát này', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (39, N'Ông/bà đánh giá về việc giải quyết khiếu nại, phản hồi của cơ quan hành chính liên quan trực tiếp đến thủ tục hành chính mà ông/bà đang thực hiện như thế nào', NULL, N'Về việc giải quyết khiếu nại, phản hồi của cơ quan hành chính liên quan trực tiếp đến thủ tục hành c', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (40, N'Chi phí có liên quan mà ông/bà phải chi trả để thực hiện thủ tục hành chính', NULL, N'Chi phí có liên quan phải chi trả ', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (41, N'Ý kiến chung của ông/bà về thủ tục hành chính vừa thực hiện', NULL, N'Ý kiến chung về thủ tục hành chính vừa thực hiện', 1, 1)
INSERT [dbo].[TieuChi] ([ID], [TenTieuChi], [NhomTieuChiID], [TenVanTat], [TypeInput], [Active]) VALUES (42, N'Ý kiến đề xuất khác của ông/bà liên quan đến việc giải quyết loại thủ tục hành chính của ông/bà', NULL, N'Ý kiến đề xuất khác', 2, 1)
SET IDENTITY_INSERT [dbo].[TieuChi] OFF
SET IDENTITY_INSERT [dbo].[TieuChiCauTraLoi] ON 

INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (13, 34, 9)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (14, 34, 21)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (15, 34, 2)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (16, 35, 9)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (17, 35, 17)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (18, 35, 1)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (19, 36, 18)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (20, 36, 4)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (21, 37, 11)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (22, 37, 19)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (23, 37, 5)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (24, 38, 20)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (25, 38, 6)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (26, 39, 12)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (27, 39, 21)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (28, 39, 2)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (29, 40, 22)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (30, 40, 13)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (31, 40, 7)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (32, 41, 9)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (33, 41, 8)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (34, 41, 23)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (41, 32, 16)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (42, 32, 10)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (43, 32, 3)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (44, 31, 9)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (45, 31, 15)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (46, 31, 2)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (49, 30, 14)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (50, 30, 1)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (54, 33, 9)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (55, 33, 21)
INSERT [dbo].[TieuChiCauTraLoi] ([ID], [TieuChiID], [CauTraLoiID]) VALUES (56, 33, 2)
SET IDENTITY_INSERT [dbo].[TieuChiCauTraLoi] OFF
ALTER TABLE [dbo].[TieuChi]  WITH CHECK ADD  CONSTRAINT [FK_ChiTieu_NhomChiTieu] FOREIGN KEY([NhomTieuChiID])
REFERENCES [dbo].[NhomTieuChi] ([ID])
GO
ALTER TABLE [dbo].[TieuChi] CHECK CONSTRAINT [FK_ChiTieu_NhomChiTieu]
GO
ALTER TABLE [dbo].[TieuChiCauTraLoi]  WITH CHECK ADD  CONSTRAINT [FK_TieuChiCauTraLoi_TieuChi] FOREIGN KEY([TieuChiID])
REFERENCES [dbo].[TieuChi] ([ID])
GO
ALTER TABLE [dbo].[TieuChiCauTraLoi] CHECK CONSTRAINT [FK_TieuChiCauTraLoi_TieuChi]
GO
ALTER TABLE [dbo].[TieuChiCauTraLoi]  WITH CHECK ADD  CONSTRAINT [FK_TieuChiCauTraLoi_TieuChiCauTraLoi] FOREIGN KEY([CauTraLoiID])
REFERENCES [dbo].[CauTraLoi] ([ID])
GO
ALTER TABLE [dbo].[TieuChiCauTraLoi] CHECK CONSTRAINT [FK_TieuChiCauTraLoi_TieuChiCauTraLoi]
GO
