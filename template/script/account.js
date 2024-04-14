document.addEventListener("DOMContentLoaded", function() {
    // Gửi yêu cầu AJAX khi trang được tải hoàn toàn
    var xhr = new XMLHttpRequest();
    var username = localStorage.getItem("username");
    console.log(username);
    var url = "http://localhost:5126/student/" + username;
    xhr.open("GET", url, true); // Thay đổi URL thành URL thích hợp của bạn
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                // Xử lý dữ liệu nhận được từ server
                var studentData = JSON.parse(xhr.responseText);
                localStorage.setItem("email",studentData.email);
                // Điền thông tin sinh viên vào các trường input
                document.getElementById("depositorName").value = studentData.fullName;
                document.getElementById("phoneNumber").value = studentData.phoneNumber;
                document.getElementById("email").value = studentData.email;
                document.getElementById("availableBalance").value = studentData.availableBalance;
                const container = document.getElementById('list-item');
                studentData.historyTransactions.forEach(data => {
                    const div = document.createElement('div');
                    div.className = 'item';
                    div.innerHTML = `
                        <p>Thời gian: ${data.date}</p>
                        <p>Số tiền: ${data.amount}</p>
                        <p>Mã số sinh viên: ${data.beneId}</p>
                        <p>Tên sinh viên: ${data.beneName}</p>
                    `;
                    container.appendChild(div);
                });
               
            } else {
                console.error("Error:", xhr.status);
            }
        }
    };
    xhr.send();
});
// document.getElementById("btn-search-stu").addEventListener("click",function(){
//     var username = document.getElementById("studentId").value;
//     var url = "http://localhost:5126/student/" + username;
//     fetch(url)
//     .then(response => {
//         if(!response.ok){
//             throw new Error('Not ok')
//         }
//         return response.json();
        
//     })
//     .then(data => {
//         document.getElementById("studentName").value = data.fullName;
//         document.getElementById("tuitionFee").value = data.tuitionFee;
//     })
// })
document.getElementById("studentId").addEventListener("input",function(){
    document.getElementById("studentName").value = "";
    document.getElementById("tuitionFee").value = "";
    var username = document.getElementById("studentId").value;
    var url = "http://localhost:5126/student/" + username;
    fetch(url)
    .then(response => {
        if(!response.ok){
            throw new Error('Not ok')
        }
        return response.json();
        
    })
    .then(data => {
        document.getElementById("studentName").value = data.fullName;
        document.getElementById("tuitionFee").value = data.tuitionFee;
    })
})

document.addEventListener('DOMContentLoaded', function () {
    var confirmButton = document.getElementById('confirmButton');
    confirmButton.addEventListener('click', function (event) {
        event.preventDefault(); // Prevent form submission

        // Kiểm tra xem các trường input có trống không
        var depositorName = document.getElementById('depositorName').value;
        var phoneNumber = document.getElementById('phoneNumber').value;
        var email = document.getElementById('email').value;
        var availableBalance = document.getElementById('availableBalance').value;
        var studentId = document.getElementById('studentId').value;
        var studentName = document.getElementById('studentName').value;
        var tuitionFee = document.getElementById('tuitionFee').value;

        // Kiểm tra nếu có trường nào trống
        if (
            !depositorName ||
            !phoneNumber ||
            !email ||
            !availableBalance ||
            !studentId ||
            !studentName ||
            !tuitionFee
        ) {
            alert('Vui lòng nhập đầy đủ thông tin.');
        } else {

            var Info = {
                username: localStorage.getItem("username"),
                amount: tuitionFee,
                email: email
            };
            // Gửi yêu cầu đến API
            fetch('http://localhost:5126/OTP', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(Info),
                
            })
            .then(response => {
                
                if (!response.ok) {
                
                    alert("Lỗi");
                }
                else{
                    localStorage.setItem('amount',tuitionFee);
                    localStorage.setItem('beni', studentId);
                    window.location.href = 'OTP.html';
                    return response.json();
                }
                
            })
            
        }
    });
    var btn_logout = document.getElementById('logout_btn');
    btn_logout.addEventListener('click', function (event){
        localStorage.clear();
        window.location.href = 'login.html';
    });

});

