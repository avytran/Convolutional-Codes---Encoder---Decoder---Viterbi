# README FOR CONVOLUTIONAL ENCODER-DECODER

## Tóm tắt

Dự án này triển khai một bộ mã hóa và giải mã bằng phương pháp mã hóa tích chập sử dụng cấu trúc trellis và thuật toán Viterbi. Nó được thiết kế để trình diễn quá trình mã hóa và giải mã các luồng dữ liệu nhị phân, tập trung vào việc phát hiện và sửa lỗi.

## Tính năng

- **Mã hóa:** Chuyển đổi dữ liệu nhị phân đầu vào thành đầu ra đã mã hóa sử dụng các đa thức sinh được chỉ định.
- **Giải mã:** Triển khai thuật toán Viterbi để giải mã dữ liệu nhị phân đã nhận và sửa bất kỳ lỗi nào.
- **Cấu trúc Trellis:** Sử dụng trellis để quản lý các trạng thái và chuyển tiếp trong quá trình giải mã.
- **Tạo Bảng Chân Trị:** Tạo bảng chân trị để ánh xạ các trạng thái đầu vào đến đầu ra tương ứng của chúng.

## Cấu trúc Mã

- **Lớp Encoder**: Xử lý việc mã hóa dữ liệu đầu vào dựa trên các bộ sinh được chỉ định.
- **Lớp Decoder**: Quản lý quá trình giải mã, bao gồm cả sửa lỗi.
- **Lớp State và NextState**: Định nghĩa cấu trúc cho các trạng thái trong trellis.
- **Lớp TruthTable**: Tạo bảng chân trị dựa trên số lượng phần tử và các hàm sinh.
- **Lớp Trellis**: Triển khai cấu trúc trellis để quản lý các chuyển tiếp trạng thái.
- **Lớp Program**: Điểm vào của ứng dụng, nơi các quá trình mã hóa 

## Đầu ra

- **Kết quả mã hóa** của dữ liệu đầu vào.
- **Mã nhận được** sau khi truyền tải.
- **Chỉ số** của bất kỳ lỗi nào được phát hiện.
- **Mã nhận được đã sửa** sau khi khắc phục.
- **Đầu ra cuối cùng** đã được giải mã.

