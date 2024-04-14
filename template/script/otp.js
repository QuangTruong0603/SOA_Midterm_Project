document.addEventListener('DOMContentLoaded', (event) => {
    // Lắng nghe sự kiện click trên nút Xác Nhận
    document.getElementById('confirmButton').addEventListener('click', function() {
        // Lấy giá trị OTP nhập vào từ input
        var otpValue = document.getElementById('otp').value;
        var Info = {
            otPcode: otpValue,
            username: localStorage.getItem("username")
           
        };
        fetch('http://localhost:5126/checkOTP',{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(Info),
        })
        .then(response => {
                
            if (!response.ok) {
            
                alert("Lỗi OTP");
            }
            else{
                var data = {
                    payerid: localStorage.getItem("username"),
                    benefiid: localStorage.getItem("beni"),
                    amount: localStorage.getItem("amount")
                }
                fetch('http://localhost:5126/Invoice',{
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(data),
                })
                .then(response => {
                    if (!response.ok) {
                        alert("Lỗi hóa đơn");
                       
                    }
                    else{
                        alert("Thành công");
                        window.location.href='account.html';
                        
                    }
                })
                
                
            }
            
        })
        
    });
});
