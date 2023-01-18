using FarmManagementSoftware.View.Windows.Thiết_lập_cây_mục_tiêu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FarmManagementSoftware.ViewModel
{
    public class DatMucTieuVM:BaseViewModel
    {
        public THAMSOCMT thamso { get; set; }

        public ICommand HoanTatCommand { get; set; }
        public ICommand HuyCommand { get; set; }

        public DatMucTieuVM() { }
        public DatMucTieuVM(THAMSOCMT thamso)
        {
            this.thamso = new THAMSOCMT();
            this.thamso.Doanhthu_muctieu = thamso.Doanhthu_muctieu;
            this.thamso.Tylede_muctieu = thamso.Tylede_muctieu;
            this.thamso.SoHeoConSinhRa_muctieu = thamso.SoHeoConSinhRa_muctieu;
            this.thamso.ODeItCon_muctieu = thamso.ODeItCon_muctieu;
            this.thamso.SoHeoConSong_MucTieu = thamso.SoHeoConSong_MucTieu;
            this.thamso.SoHeoCaiSua_muctieu = thamso.SoHeoCaiSua_muctieu;
            this.thamso.SoConChetTruocKhiCaiSua_MucTieu = thamso.SoConChetTruocKhiCaiSua_MucTieu;
            this.thamso.ThoiGianMangThai_MucTieu_Min = thamso.ThoiGianMangThai_MucTieu_Min;
            this.thamso.ThoiGianMangThai_MucTieu_Max = thamso.ThoiGianMangThai_MucTieu_Max;
            this.thamso.SoNgayCaiSua_MucTieu_Min = thamso.SoNgayCaiSua_MucTieu_Min;
            this.thamso.SoNgayCaiSua_MucTieu_Max = thamso.SoNgayCaiSua_MucTieu_Max;
            this.thamso.SoNgayKhongLamViec_MucTieu_Min = thamso.SoNgayKhongLamViec_MucTieu_Min;
            this.thamso.SoNgayKhongLamViec_MucTieu_Max = thamso.SoNgayKhongLamViec_MucTieu_Max;
            this.thamso.TrungBnhLua_MucTieu = thamso.TrungBnhLua_MucTieu;
            this.thamso.SoHeoTrongNam_MucTieu = thamso.SoHeoTrongNam_MucTieu;

            #region command bộ lọc
            HoanTatCommand = new RelayCommand<Window>((p) => { return true; }, p => {
                thamso.Doanhthu_muctieu = this.thamso.Doanhthu_muctieu;
                thamso.Tylede_muctieu = this.thamso.Tylede_muctieu;
                thamso.SoHeoConSinhRa_muctieu = this.thamso.SoHeoConSinhRa_muctieu;
                thamso.ODeItCon_muctieu = this.thamso.ODeItCon_muctieu;
                thamso.SoHeoConSong_MucTieu = this.thamso.SoHeoConSong_MucTieu;
                thamso.SoHeoCaiSua_muctieu = this.thamso.SoHeoCaiSua_muctieu;
                thamso.SoConChetTruocKhiCaiSua_MucTieu = this.thamso.SoConChetTruocKhiCaiSua_MucTieu;
                thamso.ThoiGianMangThai_MucTieu_Min = this.thamso.ThoiGianMangThai_MucTieu_Min;
                thamso.ThoiGianMangThai_MucTieu_Max = this.thamso.ThoiGianMangThai_MucTieu_Max;
                thamso.SoNgayCaiSua_MucTieu_Min = this.thamso.SoNgayCaiSua_MucTieu_Min;
                thamso.SoNgayCaiSua_MucTieu_Max = this.thamso.SoNgayCaiSua_MucTieu_Max;
                thamso.SoNgayKhongLamViec_MucTieu_Min = this.thamso.SoNgayKhongLamViec_MucTieu_Min;
                thamso.SoNgayKhongLamViec_MucTieu_Max = this.thamso.SoNgayKhongLamViec_MucTieu_Max;
                thamso.TrungBnhLua_MucTieu = this.thamso.TrungBnhLua_MucTieu;
                thamso.SoHeoTrongNam_MucTieu = this.thamso.SoHeoTrongNam_MucTieu;
                p.Close();  
            });
            #endregion

            #region command hủy
            HuyCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
            #endregion
        }
    }
}
